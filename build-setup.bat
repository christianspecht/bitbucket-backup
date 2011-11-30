@echo off

call build.bat 1

candle wix\bitbucket-backup.wxs
light -ext WixUIExtension -ext WiXNetFxExtension bitbucket-backup.wixobj -out release\msi\bitbucket-backup-%VersionNumber%.msi

pause