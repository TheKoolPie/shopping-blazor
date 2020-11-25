param (
    [Parameter(Mandatory)]
    [string]
    $FromMigrationName,
    [Parameter(Mandatory)]
    [string]
    $ToMigrationName
)

[string[]]$excludes = @("*ModelSnapshot*", "*Designer*");
[string[]]$contextNames = @("Application", "Shopping");


function ListContainsItemWithString($migrationNames, $searchTerm) {
    foreach ($item in $migrationNames) {
        if ($item -like "*$searchTerm*") {
            return $true
        }
    }
    return $false
}

function GetMigrationNames {
    param (
        [string]$contextSubFolder
    )
    [Collections.Generic.List[String]]$migrations = Get-ChildItem -Path "$serverProjectBasePath\Migrations\$contextSubFolder" -Exclude $excludes | Select-Object -ExpandProperty Name
    return $migrations
}

$serverProjectBasePath = "..\..\src\Shopping\Server\"
$scriptPath = "$serverProjectBasePath\scripts"
$scriptName = "$ToMigrationName.sql"

$targetContext = "";
foreach ($name in $contextNames) {
    $migrations = GetMigrationNames -contextSubFolder $name
    $containsFromMigration = ListContainsItemWithString -migrationNames $migrations -searchTerm $FromMigrationName
    $containsToMigration = ListContainsItemWithString -migrationNames $migrations -searchTerm $ToMigrationName

    
    if ($containsFromMigration -and $containsToMigration) {
        $targetContext = $name + "DbContext"
        break
    }
}


dotnet ef migrations script $FromMigrationName $ToMigrationName -o "$scriptPath\$scriptName" --context $targetContext --startup-project "$serverProjectBasePath\Shopping.Server.csproj"