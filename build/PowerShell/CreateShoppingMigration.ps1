
param (
    [Parameter(Mandatory)]
    [string]
    $MigrationName
)

.\CreateMigration.ps1 -MigrationName $MigrationName -MigrationContext "ShoppingDbContext" -OutputFolder "Migrations\Shopping"