@echo off
cls
SET VERSION="0.5.1"
"build\tools\rake\bin\ruby.exe" "build\tools\rake\bin\rake" %*
