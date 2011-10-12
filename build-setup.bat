@echo off

call build.bat 1

candle wix\bitbucket-backup.wxs
light -ext WixUIExtension bitbucket-backup.wixobj -out release\msi\bitbucket-backup.msi

pause