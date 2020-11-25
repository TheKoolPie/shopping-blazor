param(# Target name of migration
    [Parameter(Mandatory)]
    [string]
    $MigrationName,
    [Parameter(Mandatory)]
    [string]
    $MigrationContext,
    [Parameter(Mandatory)]
    [string]
    $OutputFolder
)

dotnet ef migrations add $MigrationName -o $OutputFolder --context $MigrationContext --startup-project "..\..\src\Shopping\Server\Shopping.Server.csproj"