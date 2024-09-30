# Use the official .NET SDK as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the .csproj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

# Publish the application
RUN dotnet publish -c Release -o /app/out

# Use a runtime image with LaTeX installed
FROM debian:bullseye-slim AS runtime

# Install required packages
RUN apt-get update && apt-get install -y \
    texlive-full \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

# Install .NET runtime
RUN apt-get update && apt-get install -y \
    aspnetcore-runtime-8.0 \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

# Set the working directory for the runtime container
WORKDIR /app

# Copy the published output from the build stage to the runtime stage
COPY --from=build /app/out .

# Set environment variables for the .NET app
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV ASPNETCORE_URLS=http://+:5000

# Expose port 5000
EXPOSE 5000

# Run the application
ENTRYPOINT ["dotnet", "YourAppName.dll"]
