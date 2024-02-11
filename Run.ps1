# Powershell scriot to start the CrpgP in local environment

$base_path = Get-Location;
$api_path = -join($base_path, "\CrpgP.WebApi");
$web_path = -join($base_path, "\CrpgP.WebApplication");
$db_container_name = "db-postgres";


# Print 
Write-Host "Starting local dev environment";
Write-Host " Database container name:   $db_container_name";
Write-Host " WebApi path:               $api_path";
Write-Host " WebApplication path:       $web_path";


# Start database container in Docker
docker container start $db_container_name


# Start API backend
Set-Location $api_path;
Start-Process dotnet run;

# Start Web site frontend
Set-Location $web_path;
Start-Process dotnet watch;


# Reset back to base path and keep window opened 
Set-Location $base_path;
Read-Host;