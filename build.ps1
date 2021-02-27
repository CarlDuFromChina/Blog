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

if (Test-Path blog\build\web) {
  Remove-Item -Recurse -Force "blog\build\web\*"
}
else
{
  New-Item -ItemType directory "blog\build\web"
}

New-Item -ItemType directory "blog\build\web\debug\"

# Restore Dotnet Packages
Write-Section-Message "Build dotnet"
nuget restore "blog\src\server_dotnet\SixpenceStudio.Blog\SixpenceStudio.Blog.sln"

# Build Dotnet files
$buildException = MSBuild.exe "blog\src\server_dotnet\SixpenceStudio.Blog\SixpenceStudio.Blog.sln"  /t:rebuild  /p:Configuration=Release
If (! $?) { Throw $buildException }
New-Item -ItemType directory "blog\build\web\bin"
$release = "blog\src\server_dotnet\build\*"
Copy-Item -Force -Recurse $release "blog\build\web\bin\"
Write-Success-Message "OK."

# Build pc.vue
Write-Section-Message "Build pc.vue."
Push-Location "blog\src\client_pc"
$npmInstallException = yarn.cmd install
If (!$?) { Throw $npmInstallException }
$buildException = yarn build --release
If (!$?) { Throw $buildException }
Pop-Location
Write-Success-Message "OK."

$release2 = "blog\src\client_pc\dist\*"
Copy-Item -Force -Recurse $release2 "blog\build\web\"

# Build mobile.vue

Write-Section-Message "Build mobile.vue."
Push-Location "blog\src\client_mobile"
$npmInstallException = yarn.cmd install
If (!$?) { Throw $npmInstallException }
$buildException = yarn build --release
If (!$?) { Throw $buildException }
Pop-Location
Write-Success-Message "OK."

$release3 = "blog\src\client_mobile\dist\*"
Copy-Item -Force -Recurse $release3 "blog\build\web\debug\"

## Compression
Write-Section-Message "Compression..."
Compress-Archive "blog\build\web\*" -DestinationPath "blog\build\web.zip" -Force

# Done
Write-Section-Message "Finished!"