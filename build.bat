@echo off
cls
SET VERSION="0.4.0.3"
"build\tools\rake\bin\ruby.exe" "build\tools\rake\bin\rake" %*
