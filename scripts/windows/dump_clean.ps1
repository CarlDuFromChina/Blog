$today=Get-Date
$7day=$($today.AddDays(-7).ToString('yyyy-MM-dd'))
 
$localdbfiles="E:\dump\$7day blog.dmp"
 
#ɾ��7��ǰ���ļ�
 
function delfiles
{
  
    #��������ת��������
    $result=Test-Path $($_) |foreach { [int] $_ }
    if ($result -eq 1) {
        del $($_)
        "ɾ���ļ��ɹ���"
        }
    else{
        "�ļ�������"
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