How to release a new version
####################################################################################

1. Increase the version number in build.bat in the main folder
(this version number is used for the setup AND the actual application)

2. Find this line right at the beginning of the bitbucket-backup.wxs (in this folder) and replace the GUID by a new one:
?define ProductId = {96C0B39F-9AC8-49D3-AB4E-18D7E88F9E3C} ?>