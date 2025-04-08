# Create wwwroot directory if it doesn't exist
if (-not (Test-Path -Path "wwwroot")) {
    New-Item -ItemType Directory -Path "wwwroot"
}

# Publish the project
dotnet publish -c Release

# Copy all files from publish directory to wwwroot
Copy-Item -Path "bin/Release/net8.0/publish/wwwroot/*" -Destination "wwwroot/" -Recurse -Force 