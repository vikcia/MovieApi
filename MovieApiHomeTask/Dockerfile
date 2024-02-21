#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MovieApiHomeTask/MovieApiHomeTask.csproj", "MovieApiHomeTask/"]
RUN dotnet restore "./MovieApiHomeTask/./MovieApiHomeTask.csproj"
COPY . .
WORKDIR "/src/MovieApiHomeTask"
RUN dotnet build "./MovieApiHomeTask.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MovieApiHomeTask.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieApiHomeTask.dll"]

FROM openjdk:8-jre-alpine

RUN mkdir /liquibase
ADD https://github.com/liquibase/liquibase/releases/download/v4.5.1/liquibase-4.5.1.tar.gz /liquibase/
RUN tar -xzf /liquibase/liquibase-4.5.1.tar.gz -C /liquibase/
RUN chmod +x /liquibase/liquibase
RUN rm /liquibase/liquibase-4.5.1.tar.gz

WORKDIR /liquibase

COPY ./changelog.xml /liquibase/

ENTRYPOINT ["./liquibase"]