# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the project files and restore dependencies
COPY ./*.csproj ./
RUN dotnet restore

# Copy the rest of the application files and build the application
COPY . ./
RUN dotnet publish -c Release -o /out

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build /out ./

# ENV DB_CONNECTION_STRING="Host=postgres_db;Port=5432;Database=rinha_db;Username=sample_user;Password=sample_password"

# Expose the port the application runs on
# ENV APP_PORT=8080
EXPOSE ${APP_PORT}

# Set the entry point for the container
ENTRYPOINT ["dotnet", "rinha-de-backend-2023.dll"]
