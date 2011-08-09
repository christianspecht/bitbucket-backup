# Bitbucket Backup

Bitbucket Backup is a backup tool which backups all your [Bitbucket](https://bitbucket.org/) repositories to your local machine.

## How does it work?

Bitbucket Backup uses the [Bitbucket API](https://api.bitbucket.org/) to get a list of all your repositories.  
Then, it uses [Mercurial](http://mercurial.selenic.com/) (which needs to be installed on your machine) to clone every repository into your local backup folder (or just pull the newest changes if it already **is** in your local backup folder)

## How to build

Just run **build.bat** in the main folder. This will create a new folder named **release** with the compiled exe.


## Setup

Bitbucket Backup loads all important values (your Bitbucket username and password, and the backup folder on your local machine) from a config file.  
Before you run Bitbucket Backup the first time, you need to edit **BitbucketBackup.exe.config** and provide your data.