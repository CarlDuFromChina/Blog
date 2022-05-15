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

Write-Section-Message "Build server"
docker build -f ".\src\server_dotnet\Blog\Blog.Business\Dockerfile" --force-rm -t carldu/blog-server:latest --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=Blog.Business" ".\src\server_dotnet\Blog"
Write-Success-Message "OK."


Write-Section-Message "Build html pc"
docker build -f ".\src\client_pc\Dockerfile" -t carldu/blog-html-pc:latest ".\src\client_pc"
Write-Success-Message "OK."

Write-Section-Message "Build html mobile"
docker build -f ".\src\client_mobile\Dockerfile" -t carldu/blog-html-mobile:latest ".\src\client_mobile"
Write-Success-Message "OK."

Write-Section-Message "Finished!"