$today=(Get-Date).ToString('yyyy-MM-dd')
pg_dump -h 127.0.0.1 -U postgres -w -F c -b -v -f "e:\dump\$today blog.dmp" blog
Write-Host "[$(Get-Date)] Finished!" -ForegroundColor Green