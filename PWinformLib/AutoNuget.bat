@ECHO OFF
nuget pack
for /f %%i in ('dir /b/a-d/o-d/t:c') do (
	set LAST=%%i
	GOTO :nextLoop1
)
:nextLoop1
nuget push %LAST% oy2kdanzp67aifznllxq5dedowcghsau3nqcbfazy7eiqq -Source https://api.nuget.org/v3/index.json