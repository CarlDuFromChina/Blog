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