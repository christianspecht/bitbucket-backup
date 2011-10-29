# Bitbucket Backup

Bitbucket Backup is a backup tool which backups all your [Bitbucket](https://bitbucket.org/) repositories to your local machine.

## How does it work?

Bitbucket Backup uses the [Bitbucket API](https://api.bitbucket.org/) to get a list of all your repositories.  
Then, it uses [Mercurial](http://mercurial.selenic.com/) and/or [Git](http://git-scm.com/) (which need to be installed on your machine if you have at least one repository of the given type) to clone every repository into your local backup folder (or just pull the newest changes if it already **is** in your local backup folder).  
It also checks for each repository, whether it has a wiki ([which is a repository itself](http://confluence.atlassian.com/display/BITBUCKET/Using+your+Bitbucket+Wiki)). If yes, that will be automatically cloned/pulled as well.

## How to build

Just run **build.bat** in the main folder. This will create a new folder named **release** with the compiled exe.

## Setup

Bitbucket Backup loads all important values (your Bitbucket username and password, and the backup folder on your local machine) from a config file.  
Before you run Bitbucket Backup the first time, you need to edit **BitbucketBackup.exe.config** and provide your data.  

Please note that Bitbucket Backup assumes that you have the Mercurial and Git executables in your **%PATH%** variable.  
(depending on the version, Git may come with a **git.exe** AND a **git.cmd** - it doesn't matter which one is in the **%PATH%**, Bitbucket Backup will find both)

### Acknowledgements

Bitbucket Backup makes use of the following open source projects:

 - [Json.NET](http://json.codeplex.com/) by James Newton-King
 - [Mercurial.Net](http://mercurialnet.codeplex.com/) by Lasse V. Karlsen
 
### License

Bitbucket Backup is licensed under the MIT License. See [License.txt](https://bitbucket.org/christianspecht/bitbucket-backup/raw/tip/License.txt) for details.