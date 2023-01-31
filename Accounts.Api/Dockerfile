#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Accounts.Api/Accounts.Api.csproj", "Accounts.Api/"]
COPY ["Accounts.Application/Accounts.Application.csproj", "Accounts.Application/"]
COPY ["Accounts.Core/Accounts.Core.csproj", "Accounts.Core/"]
COPY ["Accounts.DataAccess/Accounts.DataAccess.csproj", "Accounts.DataAccess/"]
RUN dotnet restore "Accounts.Api/Accounts.Api.csproj"
COPY . .
WORKDIR "/src/Accounts.Api"
RUN dotnet build "Accounts.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Accounts.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Accounts.Api.dll"]