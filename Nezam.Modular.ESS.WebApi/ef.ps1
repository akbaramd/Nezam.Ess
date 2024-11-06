# Define the project path for the update command
$projectPath = "..\Nezam.Modular.ESS.Identity.Infrastructure\Nezam.Modular.ESS.Identity.Infrastructure.csproj"

function Show-Menu {
    Clear-Host
    Write-Host "========================="
    Write-Host "    Entity Framework     "
    Write-Host "========================="
    Write-Host "1. Add Migration"
    Write-Host "2. Update Database"
    Write-Host "0. Exit"
}

function Add-Migration {
    param(
        [string]$projectPath
    )
    
    # Prompt for migration name
    $migrationName = Read-Host -Prompt "Enter the migration name"
    if ([string]::IsNullOrWhiteSpace($migrationName)) {
        Write-Host "Migration name cannot be empty." -ForegroundColor Red
        return
    }
    
    # Run the add migration command
    $command = "dotnet ef migrations add $migrationName -p $projectPath"
    Invoke-Expression $command
}

function Update-Database {
    param(
        [string]$projectPath
    )

    # Run the update database command
    $command = "dotnet ef database update -p $projectPath"
    Invoke-Expression $command
}

# Main script loop
do {
    Show-Menu
    $choice = Read-Host -Prompt "Please select an option"

    switch ($choice) {
        "1" {
            Add-Migration -projectPath $projectPath
        }
        "2" {
            Update-Database -projectPath $projectPath
        }
        "0" {
            Write-Host "Exiting..." -ForegroundColor Yellow
        }
        default {
            Write-Host "Invalid option. Please try again." -ForegroundColor Red
        }
    }
} while ($choice -ne "0")

