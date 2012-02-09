Bitbucket Backup is a backup tool which backups all your [Bitbucket](https://bitbucket.org/) repositories to your local machine.

## Links

- [Main Project page on Bitbucket](https://bitbucket.org/christianspecht/bitbucket-backup)
- [Download page](https://bitbucket.org/christianspecht/bitbucket-backup/downloads)

## How does it work?

Bitbucket Backup uses the [Bitbucket API](https://api.bitbucket.org/) to get a list of all your repositories.  
Then, it uses [Mercurial](http://mercurial.selenic.com/) and/or [Git](http://git-scm.com/) (which need to be installed on your machine if you have at least one repository of the given type) to clone every repository into your local backup folder (or just pull the newest changes if it already **is** in your local backup folder).  
It also checks for each repository, whether it has a wiki ([which is a repository itself](http://confluence.atlassian.com/display/BITBUCKET/Using+your+Bitbucket+Wiki)). If yes, that will be automatically cloned/pulled as well.

**DISCLAIMER: Git support is still very unstable.**  
For now, it works on my machine with Git 1.7.7 (but not on another machine with Git v1.6.5.1, for example). It definitely needs improvement.

## Setup

To install Bitbucket Backup on your machine, just run the setup.  
When you run Bitbucket Backup the first time, it will ask you for your Bitbucket username and password, and for the backup folder (must be an existing folder on your local machine).  
After that, Bitbucket Backup will run without user interaction, but you can re-enter your data any time by pressing **SPACE** on startup.

Please note that Bitbucket Backup assumes that you have the Mercurial and Git executables in your `%PATH%` variable.  
(depending on the version, Git may come with a `git.exe` AND a `git.cmd` - it doesn't matter which one is in the `%PATH%`, Bitbucket Backup will find both)

## How to build

To create a release build, just run `build.bat` or `build-setup.bat` in the main folder.  

 - `build.bat` will create a new folder named `release\bin` with the compiled exe and all necessary files.
 - `build-setup.bat` will do the same, *and* create a `release\msi` folder with a MSI setup.  

Please note that [WiX](http://wix.codeplex.com/) needs to be installed on your machine in order to build the setup file. We are using WiX 3.5 at the moment, which you can download [here](http://wix.codeplex.com/releases/view/60102).  
The build script assumes that the `bin` subfolder of the WiX installation folder is in your `%PATH%` variable.  
For more information about how to build the setup, see the readme file in the `wix` subfolder.

### Acknowledgements

Bitbucket Backup makes use of the following open source projects:

 - [Json.NET](http://json.codeplex.com/)
 - [Mercurial.Net](http://mercurialnet.codeplex.com/)
 - [MSBuild Community Tasks](http://msbuildtasks.tigris.org/)
 - [WiX](http://wix.codeplex.com/)

 The logo is based on a [floppy icon from Wikimedia Commons](http://commons.wikimedia.org/wiki/File%3aMedia-floppy.svg).
 
### License

Bitbucket Backup is licensed under the MIT License. See [License.rtf](https://bitbucket.org/christianspecht/bitbucket-backup/raw/tip/License.rtf) for details.