# 用于日志的时间戳函数
filter TIMESTAMP { "$(Get-Date) $_" }

function Write-Message { 
    Param (
        [Parameter(Mandatory = $true)]
        [String]
        $Message
    )

    Write-Host "[$(Get-Date)] $Message"
    # Write-Output $Message | TIMESTAMP
}

function Write-Section-Message { 
    Param (
        [Parameter(Mandatory = $true)]
        [String]
        $Message
    )

    Write-Host "[$(Get-Date)] $Message" -ForegroundColor Blue
}

function Write-Success-Message { 
    Param (
        [Parameter(Mandatory = $true)]
        [String]
        $Message
    )

    Write-Host "[$(Get-Date)] $Message" -ForegroundColor Green
}

function Write-Warning-Message { 
    Param (
        [Parameter(Mandatory = $true)]
        [String]
        $Message
    )

    Write-Host "[$(Get-Date)] $Message" -ForegroundColor Yellow
}

function Write-Error-Message { 
    Param (
        [Parameter(Mandatory = $true)]
        [String]
        $Message
    )

    Write-Host "[$(Get-Date)] $Message" -ForegroundColor Red
}

# Reset Build folder
If (Test-Path .\build) {
  Remove-Item -Recurse -Force ".\build\*"
}
else
{
  New-Item -ItemType directory ".\build\"
}

if (Test-Path .\build\web) {
  Remove-Item -Recurse -Force ".\build\web\*"
}
else
{
  New-Item -ItemType directory ".\build\web"
}
New-Item -ItemType directory ".\build\web\debug\"

# Restore Dotnet Packages
Write-Section-Message "Build dotnet"
nuget restore ".\src\server_dotnet\Blog\Blog.sln"

# Build Dotnet files
$buildException = MSBuild.exe ".\src\server_dotnet\Blog\Blog.sln"  /t:rebuild  /p:Configuration=Release
If (! $?) { Throw $buildException }
New-Item -ItemType directory ".\build\server"
$release = ".\src\server_dotnet\Blog\build\netcoreapp3.1\*"
Copy-Item -Force -Recurse $release ".\build\server\"
Write-Success-Message "OK."

# Build pc.vue
Write-Section-Message "Build pc.vue."
Push-Location ".\src\client_pc"
$npmInstallException = yarn.cmd install
If (!$?) { Throw $npmInstallException }
$buildException = yarn build --release
If (!$?) { Throw $buildException }
Pop-Location
Write-Success-Message "OK."

$release2 = ".\src\client_pc\dist\*"
Copy-Item -Force -Recurse $release2 ".\build\web\"

# Build mobile.vue

Write-Section-Message "Build mobile.vue."
Push-Location ".\src\client_mobile"
$npmInstallException = yarn.cmd install
If (!$?) { Throw $npmInstallException }
$buildException = yarn build --release
If (!$?) { Throw $buildException }
Pop-Location
Write-Success-Message "OK."

$release3 = ".\src\client_mobile\dist\*"
Copy-Item -Force -Recurse $release3 ".\build\web\debug\"

## Compression
Write-Section-Message "Compression..."
Compress-Archive ".\build\web\*" -DestinationPath ".\build\web.zip" -Force
Compress-Archive ".\build\server\*" -DestinationPath ".\build\server.zip" -Force
# Done
Write-Section-Message "Finished!"