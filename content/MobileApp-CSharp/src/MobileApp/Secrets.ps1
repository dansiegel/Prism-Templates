$tabSpace = "    "
$placeholder = "%%REPLACEME%%"
$secretsClass = "namespace MobileApp.Helpers`n{`n$($tabSpace)internal static class Secrets`n$($tabSpace){`n$($placeholder)`n$($tabSpace)}`n}`n"

$replacement = ""

if(Test-Path secrets.json)
{
    $secrets = Get-Content 'secrets.json' | Out-String | ConvertFrom-Json
    foreach($key in ($secrets | Get-Member -MemberType NoteProperty).Name)
    {
        $replacement += "$($tabSpace)$($tabSpace)internal const string $key = ""$($secrets.$key)"";`n`n"
    }
}

$replacement = $replacement -replace "`n`n$",""

$secretsClass = $secretsClass -replace $placeholder,$replacement

Write-Host $secretsClass

Out-File -FilePath ./Helpers/Secrets.cs -Force -InputObject $secretsClass -Encoding ASCII