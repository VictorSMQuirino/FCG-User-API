FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FCG_Users.API/FCG_Users.API.csproj", "src/FCG_Users.API/"]
COPY ["src/FCG_Users.Application/FCG_Users.Application.csproj", "src/FCG_Users.Application/"]
COPY ["src/FCG_Users.Domain/FCG_Users.Domain.csproj", "src/FCG_Users.Domain/"]
COPY ["src/FCG_Users.Infrastructure/FCG_Users.Infrastructure.csproj", "src/FCG_Users.Infrastructure/"]
RUN dotnet restore "src/FCG_Users.API/FCG_Users.API.csproj"
COPY src/ .
WORKDIR "/src/FCG_Users.API"
RUN dotnet build "./FCG_Users.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FCG_Users.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

USER root

RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
&& echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
&& wget https://download.newrelic.com/548C16BF.gpg \
&& apt-key add 548C16BF.gpg \
&& apt-get update \
&& apt-get install -y 'newrelic-dotnet-agent' \
&& rm -rf /var/lib/apt/lists/*

ENV CORECLR_ENABLE_PROFILING=1 \
CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
CORECLR_NEWRELIC_HOME=/usr/local/newrelic-dotnet-agent \
CORECLR_PROFILER_PATH=/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so \
NEW_RELIC_LICENSE_KEY=a3d439bf081e3390a2d0fa808e31aa25FFFFNRAL \
NEW_RELIC_APP_NAME="FCG_Users_API"

USER $APP_UID

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FCG_Users.API.dll"]
