
param (
    [Parameter(Mandatory)]
    [string]
    $MigrationName
)

.\CreateMigration.ps1 -MigrationName $MigrationName -MigrationContext "ApplicationDbContext" -OutputFolder "Migrations\Application"