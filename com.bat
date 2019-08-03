@echo off
:re
cls
set /p name=소스파일:

C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /out:%name%.exe %name%.cs
pause
%name%.exe
pause
goto re