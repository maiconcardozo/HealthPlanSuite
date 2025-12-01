FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

# Copy the solution file and project files
COPY Solution/ ./Solution/
COPY Src/ ./Src/

# Restore dependencies for the HealthPlan solution
RUN dotnet restore Solution/HealthPlan.sln

# Build and publish the API in Release mode
RUN dotnet publish Src/HealthPlan.API/HealthPlan.API.csproj -c Release -o /app/out /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 80

ENTRYPOINT ["dotnet", "HealthPlan.API.dll"]
