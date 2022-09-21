$today=Get-Date
$7day=$($today.AddDays(-7).ToString('yyyy-MM-dd'))
 
$localdbfiles="E:\dump\$7day blog.dmp"
 
#删除7天前的文件
 
function delfiles
{
  
    #布尔类型转换成整数
    $result=Test-Path $($_) |foreach { [int] $_ }
    if ($result -eq 1) {
        del $($_)
        "删除文件成功！"
        }
    else{
        "文件不存在"
        break
        }
}
 
 
function delfile
{
    if($args.Count -eq 0)
    {
        "No argument!"
    }
    else
    {  
           
        $args | foreach {delfiles "$($_)"}
    }
}
 
delfile $localdbfiles