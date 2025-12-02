# Steps to reproduce the hanging issue when acquiring MicrosoftIdentityTokenCredential via Dependency Injection

## Problem

When running an aspnet core app in a Docker container, attempting to acquiring the MicrosoftIdentityTokenCredential via Dependency Injection causes the application to hang.

## Prerequisites

I found the combination of the following conditions to be critical to trigger the hanging issue:
- Building the aspnet core app targeting .NET 9.0 as the target framework.
- Publishing a linux x64 self-contained, single-file executable.
- Running the application in a Docker container based on the `mcr.microsoft.com/dotnet/aspnet:10.0-alpine` image.

I couldn't tell for sure how any of these conditions individually contribute to the issue, but the combination of all three seems to reliably reproduce the hanging behavior. Changing some of them makes the problem go away.

This repo contains a sample aspnet core app can be used to reproduce the problem on Windows to help investigate the root cause.

## Steps to Reproduce

1. Set working directoy to the root of this repo.
2. Build the aspnet core app.
```powershell
dotnet build .\MicrosoftIdentityCredentialHangRepro\MicrosoftIdentityCredentialHangRepro.csproj -c Release
```
3. Publish the aspnet core app.
```powershell
dotnet publish .\MicrosoftIdentityCredentialHangRepro\MicrosoftIdentityCredentialHangRepro.csproj -c Release
```
4. Build the Docker image. I built the image by opening the solution in VS, right clicking the Dockerfile and selecting "Build Image".
5. Open Docker Desktop and run the built image by specifing the port to bind.
6. Open a web browser in the host machine and navigate to "http://localhost:<your_binding_port>/weatherforecast".
7. See in the aspnet core app logs that it hangs when trying to acquire the MicrosoftIdentityTokenCredential via Dependency Injection.
