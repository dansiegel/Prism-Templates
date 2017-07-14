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

function New-Json 
{
  [CmdletBinding()]
  param(
    [Parameter(ValueFromPipeline=$true)][HashTable]$InputObject
  )

  begin
  {
    $ser = @{}
    $jsona = @()
  }
  process
  {
    $jsoni = 
    foreach($input in $InputObject.GetEnumerator() | Where { $_.Value } ) 
    {
      if($input.Value -is [Hashtable]) 
      {
        '"'+$input.Key+'": ' + (New-JSon $input.Value)
      }
      else
      {
        $type = $input.Value.GetType()
        if(!$Ser.ContainsKey($Type)) 
        {
          $Ser.($Type) = New-Object System.Runtime.Serialization.Json.DataContractJsonSerializer $type
        }
        $stream = New-Object System.IO.MemoryStream
        $Ser.($Type).WriteObject( $stream, $Input.Value )
        '"'+$input.Key+'": ' + (Read-Stream $stream)
      }
    }

    $jsona += "{`n" +($jsoni -join ",`n")+ "`n}"
  }
  end
  {
    if($jsona.Count -gt 1)
    {
      "[$($jsona -join ",`n")]"
    } 
    else 
    {
      $jsona
    }
  }
}

Out-File -FilePath ./src/Company.MobileApp/secrets.json -Encoding ASCII -Force -InputObject "{$json\n}"