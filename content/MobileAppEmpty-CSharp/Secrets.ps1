Param(
  [string]$RootNamespace,
  [string]$ProjectPath
)

$tabSpace = "    "
$placeholder = "%%REPLACEME%%"
$secretsClass = "namespace $RootNamespace.Helpers`n{`n$($tabSpace)internal static class Secrets`n$($tabSpace){`n$($placeholder)`n$($tabSpace)}`n}`n"

$replacement = ""

if(Test-Path secrets.json)
{
    $secrets = Get-Content "$ProjectPath/secrets.json" | Out-String | ConvertFrom-Json
    foreach($key in ($secrets | Get-Member -MemberType NoteProperty).Name)
    {
        $replacement += "$($tabSpace)$($tabSpace)internal const string $key = ""$($secrets.$key)"";`n`n"
    }
}

$replacement = $replacement -replace "`n`n$",""

$secretsClass = $secretsClass -replace $placeholder,$replacement

Write-Host $secretsClass

Out-File -FilePath "$ProjectPath/Helpers/Secrets.cs" -Force -InputObject $secretsClass -Encoding ASCII