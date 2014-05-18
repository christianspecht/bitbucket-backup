@echo off

call build.bat 1 1

set wixpath=src\packages\WiX.Toolset.3.8.1128.0\tools\wix
%wixpath%\candle wix\bitbucket-backup.wxs
%wixpath%\light -ext WixUIExtension -ext WiXNetFxExtension bitbucket-backup.wixobj -out release\msi\bitbucket-backup-%VersionNumber%.msi

pause

