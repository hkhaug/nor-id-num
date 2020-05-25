param(
    [Parameter(Mandatory=$true, Position=0)]
    [string]$apiKey
)

# Clean the build directory
Remove-Item '.\build' -Recurse -ErrorAction SilentlyContinue

# Create a nupkg 
dotnet pack .\NinEngine\NinEngine.csproj --output .\build

# Get a reference to the nupkg because the version number is not known for this script
$result = Get-Item -Path .\build\*.nupkg
if($result.Count -gt 1){
    Write-Error "More than 1 nupkg package found. Make sure the build directory is cleaned correctly"
    Exit
}
$nupkg = $result | Select-Object -First 1

# Pushing it to nuget
dotnet nuget push $nupkg.FullName --api-key $apiKey --source https://api.nuget.org/v3/index.json