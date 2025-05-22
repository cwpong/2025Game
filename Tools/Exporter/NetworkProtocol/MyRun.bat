@echo off
setlocal enabledelayedexpansion

REM 获取脚本所在目录
set "ScriptDir=%~dp0"
REM 去掉末尾的反斜杠
set "ScriptDir=%ScriptDir:~0,-1%"

:menu
echo Please select an option:
echo 1. Client
echo 2. Server
echo 3. All

set "choice="
set /p "choice=Please select an option (1-3): "

REM 验证输入
if not defined choice (
    echo Error: No input detected
    goto :menu
)

if "%choice%"=="1" (
    echo Running Client...
    if exist "%ScriptDir%\Fantasy.Tools.NetworkProtocol.dll" (
        dotnet "%ScriptDir%\Fantasy.Tools.NetworkProtocol.dll" --p 1 --f "%ScriptDir%"
    ) else (
        echo Error: DLL file not found
    )
) else if "%choice%"=="2" (
    echo Running Server...
    if exist "%ScriptDir%\Fantasy.Tools.NetworkProtocol.dll" (
        dotnet "%ScriptDir%\Fantasy.Tools.NetworkProtocol.dll" --p 2 --f "%ScriptDir%"
    ) else (
        echo Error: DLL file not found
    )
) else if "%choice%"=="3" (
    echo Running All...
    if exist "%ScriptDir%\Fantasy.Tools.NetworkProtocol.dll" (
        dotnet "%ScriptDir%\Fantasy.Tools.NetworkProtocol.dll" --p 3 --f "%ScriptDir%"
    ) else (
        echo Error: DLL file not found
    )
) else (
    echo Invalid option. Please select 1, 2 or 3.
    goto :menu
)

endlocal