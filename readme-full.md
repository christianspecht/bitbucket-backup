![logo](https://raw.githubusercontent.com/christianspecht/bitbucket-backup/master/img/logo128x128.png)

Bitbucket Backup is a backup tool which clones/pulls all your [Bitbucket](https://bitbucket.org/) repositories to your local machine.

> ## Please note:
> 
> **Development of Bitbucket Backup has stopped, please use [SCM Backup](https://scm-backup.org/) instead.**
> 
> SCM Backup can do everything Bitbucket Backup can, plus:
> 
> - it supports more hosters (at the moment: GitHub, Bitbucket)
> - it's able to backup multiple users/teams per hoster
> - specific repositories can be excluded from backup
> 
> For more information, please read:
> 
> - [Documentation how to use SCM Backup (for users of Bitbucket Backup)](https://docs.scm-backup.org/en/latest/bitbucket-backup-users/)
> - [Why Bitbucket Backup was retired](https://christianspecht.de/2018/08/01/goodbye-bitbucket-backup-hello-scm-backup/)
>
> **Plus, version 1 of the Bitbucket API (which Bitbucket Backup uses) is deprecated and will be removed on April 12, 2019 ([read the announcement here](https://developer.atlassian.com/cloud/bitbucket/deprecation-notice-v1-apis/)), which means that Bitbucket Backup will stop working on April 12, 2019.**

---

## Links

- [Download page](https://github.com/christianspecht/bitbucket-backup/releases)
- [Report a bug](https://github.com/christianspecht/bitbucket-backup/issues/new)
- [Main project page on GitHub](https://github.com/christianspecht/bitbucket-backup)

---

## How does it work?

Bitbucket Backup uses the [Bitbucket API](https://api.bitbucket.org/) to get a list of all your repositories.  
Then, it uses [Mercurial](http://mercurial.selenic.com/) and/or [Git](http://git-scm.com/) (which need to be installed on your machine if you have at least one repository of the given type) to clone every repository into your local backup folder (or just pull the newest changes if it already **is** in your local backup folder).  
It also checks for each repository, whether it has a wiki ([which is a repository itself](https://confluence.atlassian.com/display/BITBUCKET/Use+a+wiki)). If yes, that will be automatically cloned/pulled as well.

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
If you want a "portable" version without installer, there's a .zip download with the binaries as well *(starting with version 1.3.2)*.

When you run Bitbucket Backup the first time, it will ask you for:

0. Your Bitbucket username and **a previously created app password** *(see [next section](#app-password) for more info)*  
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

<div id="app-password"></div>
## Creating an app password

Go into your Bitbucket account settings and [create an app password](https://confluence.atlassian.com/bitbucket/app-passwords-828781300.html) for Bitbucket Backup. 

You need to check the following permissions:

- Account: Read
- Repositories: Read
- Wikis: Read and write
 
![app password settings](https://raw.githubusercontent.com/christianspecht/bitbucket-backup/master/img/app-password.png)

Bitbucket will then create a password for you. You must enter **this password** into Bitbucket Backup!

#### *Background info for long-time users of Bitbucket Backup:*

*In mid 2016, Atlassian/Bitbucket changed two things:*

0. *They enabled a new feature: [app passwords](https://blog.bitbucket.org/2016/06/06/app-passwords-bitbucket-cloud/)*
0. *They [auto-upgraded all Bitbucket accounts to Atlassian accounts](https://confluence.atlassian.com/bitbucket/upgrade-to-atlassian-account-829056056.html)*  

*At the moment when you log into Bitbucket and change your Bitbucket account to an Atlassian account, your old Bitbucket password doesn't exist anymore. So Bitbucket Backup (which used the old password) stops working until you create an app password with the proper permissions, and use **that** in Bitbucket Backup.*


---

## Emailing output

To support the use case where Bitbucket Backup is scheduled to run as a service task, Bitbucket Backup provides an option that you may use to email the output. To take advantage of this, you will need to configure your SMTP settings in the application configuration file. Instructions on how to do this can be found on the [SMTP settings MSDN documentation page](https://msdn.microsoft.com/en-us/library/ms164240(v=vs.110).aspx).

After the application configuration file is updated, run the application with the --email parameter specifying the email address to send the output to, e.g. `--email administrator@mycompany.com`.

---

<div id="restore"></div>
## What is actually backed up and how do I restore from that backup?

Bitbucket Backup creates local repositories and pulls from the remote Bitbucket repositories into the local ones.  
Those local repositories are **bare repositories**, i.e. they don't contain a working directory.  

When you look inside the repository directories, you'll see this:

- **Mercurial**: A single folder named `.hg`
- **Git**: Some folders (`objects`, `refs`, ...) and some files

**Your complete history and your source code are in there - you just don't see the actual files.**  
The repository is backed up without a working directory, because it's not necessary. All the data already exists inside the repository, a second copy of everything in the working directory would just be a waste of space.

The easiest way to restore your working directory is to clone the bare repository that Bitbucket Backup created *(called `bare-repo` in the examples)*, which will create a clone **with** a working directory *(called `working-repo` in the examples)*:

#### Mercurial:

	hg clone bare-repo working-repo 

It's also possible to create the working directory directly inside the bare repository:

	cd bare-repo
	hg update tip

#### Git:

	git clone bare-repo.git working-repo

*(Note: It seems the Git tools do not like cloning from a bare repository that does not end in the `.git` extension, so Bitbucket Backup automatically appends it to all Git repositories)* 


 
For more background information *(and the discussion that led to the creation of this section)* see [this issue](https://github.com/christianspecht/bitbucket-backup/issues/22).

---

## Development

#### How to build

To create a release build, just run `build.bat` or `build-setup.bat` in the main folder.  

 - `build.bat` will create a new folder named `release\bin` with the compiled exe and all necessary files.
 - `build-setup.bat` will do the same, *and* additionally create:
   - a `release\msi` folder with a MSI setup
   - a `release\zip` folder with a ZIP file *(containing the content of the `release\bin` folder)*

---

### Acknowledgements

Bitbucket Backup makes use of the following open source projects:

 - [Fluent Command Line Parser](http://fclp.github.io/fluent-command-line-parser/)
 - [Json.NET](http://www.newtonsoft.com/json)
 - [Mercurial.Net](http://mercurialnet.codeplex.com/)
 - [MSBuild Community Tasks](https://github.com/loresoft/msbuildtasks)
 - [NuGet](http://www.nuget.org/)
 - [Ninject](http://ninject.org/)
 - [RestSharp](http://restsharp.org/)
 - [WiX](http://wixtoolset.org/)

 The logo is based on a [floppy icon from Wikimedia Commons](http://commons.wikimedia.org/wiki/File%3aMedia-floppy.svg).

---

### Contributors

- [Drew Peterson](https://bitbucket.org/drewpeterson)
- [Jose Araujo](https://bitbucket.org/josea)
- [Matt Berther](http://matt.berther.io/)
- [Nassere Besseghir](https://bitbucket.org/nbesseghircsc)

---

<div id="license"></div>
### License

Bitbucket Backup is licensed under the MIT License. See [License.rtf](https://github.com/christianspecht/bitbucket-backup/raw/master/License.rtf) for details.

---

### Project Info

<script type='text/javascript' src='https://www.openhub.net/p/bitbucket-backup/widgets/project_basic_stats?format=js'></script>
<script type='text/javascript' src='https://www.openhub.net/p/bitbucket-backup/widgets/project_languages?format=js'></script>