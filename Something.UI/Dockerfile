#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Something.UI/Something.UI.csproj", "Something.UI/"]
RUN dotnet restore "Something.UI/Something.UI.csproj"
COPY . .
WORKDIR "/src/Something.UI"
RUN dotnet build "Something.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Something.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Something.UI.dll", "/d"]