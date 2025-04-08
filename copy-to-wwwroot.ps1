# Create blazor-dist/wwwroot directory if it doesn't exist
if (-not (Test-Path -Path "blazor-dist/wwwroot")) {
    New-Item -ItemType Directory -Path "blazor-dist/wwwroot" -Force
}

# Publish the project
dotnet publish -c Release

# Copy all files from publish directory to blazor-dist/wwwroot
Copy-Item -Path "bin/Release/net8.0/publish/wwwroot/*" -Destination "blazor-dist/wwwroot/" -Recurse -Force 