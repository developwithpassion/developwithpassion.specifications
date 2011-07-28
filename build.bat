@echo off
cls
SET VERSION="0.4.21.1"
"build\tools\rake\bin\ruby.exe" "build\tools\rake\bin\rake" %*
