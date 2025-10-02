# Use .NET 7 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy solution and project files
COPY TreffServices.sln ./
COPY WebApi/WebApi.csproj WebApi/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Persistence/Persistence.csproj Persistence/

# Restore dependencies
RUN dotnet restore WebApi/WebApi.csproj

# Copy the full source tree
COPY . .

# Build and publish the application
RUN dotnet publish WebApi/WebApi.csproj -c Release -o /app/publish /p:UseAppHost=false

# Use .NET 7 ASP.NET Core runtime for final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080:8088

# Create a non-root user for security
RUN getent group app || addgroup --system app; \
    getent passwd app || adduser --system app --ingroup app
# Copy published app from build image
COPY --from=build /app/publish ./

# Set ownership and drop privileges
RUN chown -R app:app /app
USER app

ENTRYPOINT ["dotnet", "WebApi.dll"]
