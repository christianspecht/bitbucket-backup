![logo](https://bitbucket.org/christianspecht/bitbucket-backup/raw/tip/img/logo128x128.png)

Bitbucket Backup is a backup tool which clones/pulls all your [Bitbucket](https://bitbucket.org/) repositories to your local machine.

---

## Links

- [Download page](https://bitbucket.org/christianspecht/bitbucket-backup/downloads)
- [Report a bug](https://bitbucket.org/christianspecht/bitbucket-backup/issues/new)
- [Main project page on Bitbucket](https://bitbucket.org/christianspecht/bitbucket-backup)

---

## How does it work?

Bitbucket Backup uses the [Bitbucket API](https://api.bitbucket.org/) to get a list of all your repositories.  
Then, it uses [Mercurial](http://mercurial.selenic.com/) and/or [Git](http://git-scm.com/) (which need to be installed on your machine if you have at least one repository of the given type) to clone every repository into your local backup folder (or just pull the newest changes if it already **is** in your local backup folder).  
It also checks for each repository, whether it has a wiki ([which is a repository itself](http://confluence.atlassian.com/display/BITBUCKET/Using+a+bitbucket+Wiki)). If yes, that will be automatically cloned/pulled as well.

---

## Setup

System requirements:

- Windows *(at least XP)*
- [.NET Framework 4 Client Profile](http://www.microsoft.com/en-us/download/details.aspx?id=17113) *(of course it works with the full framework as well, but Client Profile is enough)*
- [Mercurial](http://mercurial.selenic.com/) *(any version)* if you have Mercurial repositories
- [Git](http://git-scm.com/) *(at least version 1.7.x)* if you have Git repositories

Please note that Bitbucket Backup assumes that you have the Mercurial and Git executables in your `%PATH%` variable.  
(depending on the version, Git may come with a `git.exe` AND a `git.cmd` - it doesn't matter which one is in the `%PATH%`, Bitbucket Backup will find both)

To install Bitbucket Backup on your machine, just run the setup.  
When you run Bitbucket Backup the first time, it will ask you for:

0. Your Bitbucket username and password  
*This user's repositories will be backed up.*

0. Your Bitbucket team name (optional)  
*If entered, **the repositories of that team will be backed up instead of the user's repositories**.  
The user is still needed for authentication.*

0. A backup folder on your local machine  
*The folder must already exist, Bitbucket Backup won't create it!*

0. Timeout for pulling (optional, Mercurial only)  
*By default, Mercurial times out after 60 seconds. You may want to increase that value if you have large repositories or a slow connection.*

After that, Bitbucket Backup will run without user interaction, but you can re-enter your data any time by pressing **SPACE** on startup.

---

## How to build

To create a release build, just run `build.bat` or `build-setup.bat` in the main folder.  

 - `build.bat` will create a new folder named `release\bin` with the compiled exe and all necessary files.
 - `build-setup.bat` will do the same, *and* create a `release\msi` folder with a MSI setup.  

Please note that [WiX](http://wixtoolset.org/) needs to be installed on your machine in order to build the setup file. We are using WiX 3.5 at the moment, which you can download [here](http://wix.codeplex.com/releases/view/60102).  
The build script assumes that the `bin` subfolder of the WiX installation folder is in your `%PATH%` variable.

---

### Acknowledgements

Bitbucket Backup makes use of the following open source projects:

 - [Json.NET](http://json.codeplex.com/)
 - [Mercurial.Net](http://mercurialnet.codeplex.com/)
 - [MSBuild Community Tasks](https://github.com/loresoft/msbuildtasks)
 - [Ninject](http://ninject.org/)
 - [RestSharp](http://restsharp.org/)
 - [WiX](http://wixtoolset.org/)

 The logo is based on a [floppy icon from Wikimedia Commons](http://commons.wikimedia.org/wiki/File%3aMedia-floppy.svg).

---

### Contributors

- [Drew Peterson](https://bitbucket.org/drewpeterson)
- [José Araujo](https://bitbucket.org/josea)
- [Nassere Besseghir](https://bitbucket.org/nbesseghircsc)

---

### License

Bitbucket Backup is licensed under the MIT License. See [License.rtf](https://bitbucket.org/christianspecht/bitbucket-backup/raw/tip/License.rtf) for details.

---

### Project Info

<script type="text/javascript" src="http://www.ohloh.net/p/585509/widgets/project_basic_stats.js"></script>  
<script type="text/javascript" src="http://www.ohloh.net/p/585509/widgets/project_languages.js"></script>