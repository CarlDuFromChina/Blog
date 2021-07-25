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
If (Test-Path .\release) {
  Remove-Item -Recurse -Force ".\release\*"
}
else
{
  New-Item -ItemType directory ".\release\"
}

if (Test-Path .\release\web) {
  Remove-Item -Recurse -Force ".\release\web\*"
}
else
{
  New-Item -ItemType directory ".\release\web"
}
New-Item -ItemType directory ".\release\web\debug\"

# Restore Dotnet Packages
Write-Section-Message "Build dotnet"
Remove-Item -Recurse -Force ".\src\server_dotnet\Blog\release\netcoreapp3.1\*"

# Build Dotnet files
$buildException = dotnet build ".\src\server_dotnet\Blog\Blog.sln" -f netcoreapp3.1 -r win-x64 -c Release --no-incremental
If (! $?) { Throw $buildException }
New-Item -ItemType directory ".\release\server"
$release = ".\src\server_dotnet\Blog\release\netcoreapp3.1\win-x64\*"
Copy-Item -Force -Recurse $release ".\release\server\"
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
Copy-Item -Force -Recurse $release2 ".\release\web\"

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
Copy-Item -Force -Recurse $release3 ".\release\web\debug\"

## Compression
Write-Section-Message "Compression..."
Compress-Archive ".\release\web\*" -DestinationPath ".\release\web.zip" -Force
Compress-Archive ".\release\server\*" -DestinationPath ".\release\server.zip" -Force
# Done
Write-Section-Message "Finished!"