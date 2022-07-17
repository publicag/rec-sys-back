FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RecommendationSystem.API/RecommendationSystem.API.csproj", "RecommendationSystem.API/"]
RUN dotnet restore "RecommendationSystem.API\RecommendationSystem.API.csproj"
COPY . .
WORKDIR "/src/RecommendationSystem.API"
RUN dotnet build "RecommendationSystem.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RecommendationSystem.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RecommendationSystem.API.dll"]