# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS base
USER root
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
# libc6-compat is necessary to run the pre-built linux-x64 binary
RUN apk add --no-cache libc6-compat
WORKDIR /app
COPY MicrosoftIdentityCredentialHangRepro/bin/Release/net9.0/linux-x64/publish/ .
ENTRYPOINT ["./MicrosoftIdentityCredentialHangRepro"]