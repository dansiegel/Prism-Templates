$secretNames = @(
#if (UseMobileCenter)
  "MobileCenter_iOS_Secret",
  "MobileCenter_Android_Secret",
  "MobileCenter_UWP_Secret",
#endif
#if (HasAuthClientId)
  "AuthClientId",
#endif
#if (UseAzureMobileClient)
  "AppServiceEndpoint",
  "AlternateLoginHost",
  "LoginUriPrefix"
#else
  "AppServiceEndpoint"
#endif
)

$jsonValues = @()
foreach($secretName in $secretNames)
{
  $secretValue = (get-item env:$secretName).Value
  $jsonValues += '"' + $secretName + '": "' + $secretValue + '"'
}

Out-File -FilePath ./src/Company.MobileApp/secrets.json -Encoding ASCII -Force -InputObject "{$($jsonValues -join ',')}"