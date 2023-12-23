FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /src
COPY ["./Superhero.Api/*.csproj", "."]
COPY ["./Superhero.Api.Tests/*.csproj", "."]
RUN dotnet restore "./Superhero.Api.Tests.csproj"
RUN dotnet restore "./Superhero.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Superhero.Api/Superhero.Api.csproj" -c Release -o /app/build
RUN dotnet build "./Superhero.Api.Tests/Superhero.Api.Tests.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "./Superhero.Api/Superhero.Api.csproj" -c Release -o /app/publish /p:UseApphost=false
RUN dotnet publish "./Superhero.Api.Tests/Superhero.Api.Tests.csproj" -c Release -o /app/publish /p:UseApphost=false

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Superhero.Api.dll"]