# Use .NET 10 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["K8sLearningApi.csproj", "./"]
RUN dotnet restore "K8sLearningApi.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "K8sLearningApi.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "K8sLearningApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copy published app
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "K8sLearningApi.dll"]

