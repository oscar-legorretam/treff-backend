# ---- Build args (override if needed) ----
ARG DOTNET_VERSION=8.0

# ---- Base runtime image ----
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-alpine AS runtime
ENV ASPNETCORE_URLS=http://+:8080
# If you rely on localization/ICU, comment the next line:
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
EXPOSE 8080

# Create non-root user (check if group exists before creating)
RUN getent group app || addgroup -S app; \
    getent passwd app || adduser -S app -G app
WORKDIR /app

# ---- Build image ----
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION}-alpine AS build
WORKDIR /src

# Copy only csproj files first to leverage Docker layer caching.
# Adjust names if your repo uses different project names.
COPY WebApi/WebApi.csproj WebApi/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Persistence/Persistence.csproj Persistence/
# (Optional) copy a solution file if you have one
COPY TreffServices.sln ./

# Restore
RUN dotnet restore WebApi/WebApi.csproj

# Copy the rest of the source
COPY . .

# Publish (trim AppHost for smaller image)
RUN dotnet publish WebApi/WebApi.csproj -c Release -o /app/publish /p:UseAppHost=false

# ---- Final image ----
FROM runtime AS final
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish ./

# Ensure files are owned by 'app' and drop privileges
RUN chown -R app:app /app
USER app

# (Optional) healthcheck — replace /health with your endpoint if available
# HEALTHCHECK --interval=30s --timeout=3s --start-period=20s --retries=3 
#   CMD wget -qO- http://127.0.0.1:8080/health || exit 1

ENTRYPOINT ["dotnet", "WebApi.dll"]
