@echo off
cls
SET VERSION="0.4.21.4"
"build\tools\rake\bin\ruby.exe" "build\tools\rake\bin\rake" %*
