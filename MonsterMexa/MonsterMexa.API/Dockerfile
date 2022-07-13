#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MonsterMexa.API/MonsterMexa.API.csproj", "MonsterMexa.API/"]
COPY ["MonsterMexa.BusinessLogic/MonsterMexa.BusinessLogic.csproj", "MonsterMexa.BusinessLogic/"]
COPY ["MonsterMexa.Domain/MonsterMexa.Domain.csproj", "MonsterMexa.Domain/"]
COPY ["MonsterMexa.DataAccess.Postgres/MonsterMexa.DataAccess.Postgres.csproj", "MonsterMexa.DataAccess.Postgres/"]
RUN dotnet restore "MonsterMexa.API/MonsterMexa.API.csproj"
COPY . .
WORKDIR "/src/MonsterMexa.API"
RUN dotnet build "MonsterMexa.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MonsterMexa.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MonsterMexa.API.dll"]