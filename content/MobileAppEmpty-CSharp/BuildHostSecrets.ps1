# You can create as many secrets as you want by adding the name to the 
# secretNames array.
# 
# $secretNames = @(
#   "ClientId",
#   "ClientSecret",
#   "AuthEndpoint"
# )

$secretNames = @(
  "SampleSecret"
)

$jsonValues = @()
foreach($secretName in $secretNames)
{
  if(Test-Path env:$secretName)
  {
    $secretValue = (get-item env:$secretName).Value
    $jsonValues += '"' + $secretName + '": "' + $secretValue + '"'
  }
  else 
  {
    Write-Host "$secretName has not been defined"
  }
}

$secretFilePath = "./src/Company.MobileApp/secrets.json"

# Avoid overwriting a secret.json that may exist on a developer machine.
if(-not (Test-Path -Path $secretFilePath))
{
  Out-File -FilePath $secretFilePath  -Encoding ASCII -Force -InputObject "{$($jsonValues -join ',')}"
}