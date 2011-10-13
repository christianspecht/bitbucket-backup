# Bitbucket Backup

Bitbucket Backup is a backup tool which backups all your [Bitbucket](https://bitbucket.org/) repositories to your local machine.

## How does it work?

Bitbucket Backup uses the [Bitbucket API](https://api.bitbucket.org/) to get a list of all your repositories.  
Then, it uses [Mercurial](http://mercurial.selenic.com/) (which needs to be installed on your machine) to clone every repository into your local backup folder (or just pull the newest changes if it already **is** in your local backup folder).  
It also checks for each repository, whether it has a wiki ([which is a Mercurial repository itself](http://confluence.atlassian.com/display/BITBUCKET/Using+your+Bitbucket+Wiki)). If yes, that will be automatically cloned/pulled as well.

## Setup

Bitbucket Backup loads all important values (your Bitbucket username and password, and the backup folder on your local machine) from a config file.  
Before you run Bitbucket Backup the first time, you need to edit **BitbucketBackup.exe.config** and provide your data.

## How to build

To create a release build, just run **build.bat** or **build-setup.bat** in the main folder.  

 - **build.bat** will create a new folder named **release\bin** with the compiled exe and all necessary files.
 - **build-setup.bat** will do the same, *and* create a **release\msi** folder with a MSI setup.  

Please note that [WiX](http://wix.codeplex.com/) needs to be installed on your machine in order to build the setup file. We are using WiX 3.5 at the moment, which you can download [here](http://wix.codeplex.com/releases/view/60102).  
The build script assumes that the **bin** subfolder of the WiX installation folder is in your **%PATH%** variable.

### Acknowledgements

Bitbucket Backup makes use of the following open source projects:

 - [Json.NET](http://json.codeplex.com/)
 - [Mercurial.Net](http://mercurialnet.codeplex.com/)
 - [MSBuild Community Tasks](http://msbuildtasks.tigris.org/)
 - [WiX](http://wix.codeplex.com/)
 
### License

Bitbucket Backup is licensed under the MIT License. See [License.rtf](https://bitbucket.org/christianspecht/bitbucket-backup/raw/tip/License.rtf) for details.