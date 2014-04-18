@echo off

rem load version info (see version_number.bat for more info!)
call wix\version_number.bat

rem path to msbuild.exe
path=%path%;%windir%\Microsoft.net\Framework\v4.0.30319

rem go to current folder
cd %~dp0

msbuild build.proj /p:CreateZip="%2"

rem To change the output folder, use the following parameter: /p:BuildDir=C:\BuildTest

if "1" == "%1" goto exit

pause

:exit