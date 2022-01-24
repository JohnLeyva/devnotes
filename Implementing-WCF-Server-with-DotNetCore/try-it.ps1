Clear-Host

$currentFolder = Split-Path ( Get-PSCallStack )[0].ScriptName -Parent

function Get-LastFile($name) {
    $ff = Get-ChildItem  -Path $currentFolder -fi $name -Recurse | 
    Sort-Object -Property LastWriteTime | Select-Object -Last 1
    $ff.FullName
}

$WcfServer = Get-LastFile -name "WcfServer.exe"

. $WcfServer

dotnet run --project $currentFolder\WcfClientDotNetCore\WcfClientDotNetCore.csproj
dotnet run --project $currentFolder\CoreWcfServer\CoreWcfServer.csproj


