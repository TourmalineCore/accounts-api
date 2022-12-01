#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UserManagementService.Api/UserManagementService.Api.csproj", "UserManagementService.Api/"]
COPY ["UserManagementService.Application/UserManagementService.Application.csproj", "UserManagementService.Application/"]
COPY ["UserManagementService.Core/UserManagementService.Core.csproj", "UserManagementService.Core/"]
COPY ["UserManagementService.DataAccess/UserManagementService.DataAccess.csproj", "UserManagementService.DataAccess/"]
RUN dotnet restore "UserManagementService.Api/UserManagementService.Api.csproj"
COPY . .
WORKDIR "/src/UserManagementService.Api"
RUN dotnet build "UserManagementService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserManagementService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagementService.Api.dll"]