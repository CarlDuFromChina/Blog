# loading helper functions
. ".\help.ps1"

# Restore Dotnet Packages
Write-Section-Message "Restore Dotnet Packages"
nuget restore blog\src\dotnet\SixpenceStudio.Blog\SixpenceStudio.Blog.sln
Write-Success-Message "OK."

# Build Dotnet files
Write-Section-Message "Build Dotnet files"
$buildException = MSBuild.exe "blog\src\dotnet\SixpenceStudio.Blog\SixpenceStudio.Blog.sln"  /t:rebuild  /p:Configuration=Release
If (! $?) { Throw $buildException }
Write-Success-Message "OK."

# Build pc.vue
Write-Section-Message "Build pc.vue."
Push-Location "blog\src\client_pc"
$npmInstallException = yarn.cmd install
If (!$?) { Throw $npmInstallException }
$buildException = yarn build
If (!$?) { Throw $buildException }
Pop-Location
Write-Success-Message "OK."

# Reset Build folder
If (Test-Path \blog\build) { Remove-Item -Recurse -Force "blog\build\*" }
else
{
  New-Item -ItemType directory "blog\build\"
}

New-Item -ItemType directory "blog\build\bin"

$release = 'blog\src\dotnet\build\*'
$release2 = 'blog\src\client_pc\dist\*'
Copy-Item -Force -Recurse $release 'blog\build\bin\'
Copy-Item -Force -Recurse $release2 'blog\build\'
Remove-Item -Recurse -Force "blog\build\bin\*.pdb"

# Done
Write-Section-Message "Finished!"