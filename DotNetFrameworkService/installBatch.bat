@echo off

set Directory="FilePath.exe"
set ServiceName=Notification Service

goto check_permissions

:check_permissions
   call :echoyellow "Detecting and verifying current cmd permissions..."

   net session >nul 2>&1
   if %errorLevel% == 0 (
       call :echogreen "Success: Administrative permissions confirmed."
        goto start
   ) else (
	   call :echored = "ERROR: Administrator Access Required"
	   echo. 
       echo This script must be run as administrator to work properly!
       echo.
       PAUSE
       goto exit
   )

:start
    echo.
call :echoblue "%ServiceName% Setup Process"

call :echoyellow Uninstalling the service..."
%Directory% stop uninstall
call :echogreen "Success: %ServiceName% is now uninstalled!"

call :echoyellow "Ready to Install %ServiceName%. Continue?"
PAUSE
%Directory% install
call :echogreen "Success: %ServiceName% is now installed!"
%Directory% start
call :echogreen "Success: %ServiceName% is now started!"
goto end

:echoyellow
%Windir%\System32\WindowsPowerShell\v1.0\Powershell.exe write-host -foregroundcolor Yellow %1
goto:eof

:echored
%Windir%\System32\WindowsPowerShell\v1.0\Powershell.exe write-host -foregroundcolor Red %1
goto:eof

:echogreen
%Windir%\System32\WindowsPowerShell\v1.0\Powershell.exe write-host -foregroundcolor Green %1
goto:eof

:echoblue
%Windir%\System32\WindowsPowerShell\v1.0\Powershell.exe write-host -foregroundcolor Cyan %1
goto:eof

:end
call :echoblue "%ServiceName% Running"
PAUSE
goto exit

:exit
EXIT /B 1
