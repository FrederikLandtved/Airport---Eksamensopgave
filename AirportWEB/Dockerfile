FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ["AirportWEB/AirportWEB.csproj", "AirportWEB/"]
COPY ["AirportAPI/AirportAPI.csproj", "AirportAPI/"]
RUN dotnet restore "AirportWEB/AirportWEB.csproj"
COPY . .
WORKDIR "/src/AirportWEB"
RUN dotnet build "AirportWEB.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AirportWEB.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AirportWEB.dll"]