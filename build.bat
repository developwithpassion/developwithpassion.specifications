@echo off
cls
SET VERSION="0.4.24.2"
"build\tools\rake\bin\ruby.exe" "build\tools\rake\bin\rake" %*
