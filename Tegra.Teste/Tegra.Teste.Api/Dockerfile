#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Tegra.Teste.Api/Tegra.Teste.Api.csproj", "Tegra.Teste.Api/"]
RUN dotnet restore "Tegra.Teste.Api/Tegra.Teste.Api.csproj"
COPY . .
WORKDIR "/src/Tegra.Teste.Api"
RUN dotnet build "Tegra.Teste.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tegra.Teste.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tegra.Teste.Api.dll"]