# loading helper functions
. ".\help.ps1"

# Reset Build folder
If (Test-Path blog\build) {
  Remove-Item -Recurse -Force "blog\build\*"
}
else
{
  New-Item -ItemType directory "blog\build\"
}

# Restore Dotnet Packages
@'
. ".\help.ps1"
Write-Section-Message "Build dotnet"
nuget restore "blog\src\dotnet\SixpenceStudio.Blog\SixpenceStudio.Blog.sln"

# Build Dotnet files
$buildException = MSBuild.exe "blog\src\dotnet\SixpenceStudio.Blog\SixpenceStudio.Blog.sln"  /t:rebuild  /p:Configuration=Release
If (! $?) { Throw $buildException }
New-Item -ItemType directory "blog\build\bin"
$release = "blog\src\dotnet\build\*"
Copy-Item -Force -Recurse $release "blog\build\bin\"
Remove-Item -Recurse -Force "blog\build\bin\*.pdb"
Write-Success-Message "OK."
'@ > DotNetBuild.ps1

start powershell .\DotNetBuild.ps1

# Build pc.vue
@'
. ".\help.ps1"
Write-Section-Message "Build pc.vue."
Push-Location "blog\src\client_pc"
$npmInstallException = yarn.cmd install
If (!$?) { Throw $npmInstallException }
$buildException = yarn build
If (!$?) { Throw $buildException }
Pop-Location
Write-Success-Message "OK."

$release2 = "blog\src\client_pc\dist\*"
Copy-Item -Force -Recurse $release2 "blog\build\"
'@ > ClientPCBuild.ps1

start powershell .\ClientPCBuild.ps1 -wait

# Done
Remove-Item -Recurse -Force ".\DotNetBuild.ps1"
Remove-Item -Recurse -Force ".\ClientPCBuild.ps1"
Write-Section-Message "Finished!"