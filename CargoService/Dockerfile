FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Install System.Drawing native dependencies
RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
       libc6-dev \
       libgdiplus \
       libx11-dev \
    && rm -rf /var/lib/apt/lists/*

# Install Microsoft core fonts
RUN echo "deb http://deb.debian.org/debian stable main contrib non-free" > /etc/apt/sources.list \
    && echo "ttf-mscorefonts-installer msttcorefonts/accepted-mscorefonts-eula select true" | debconf-set-selections \
    && apt-get update \
    && apt-get install -y \
        ttf-mscorefonts-installer \
    && apt-get clean \
    && apt-get autoremove -y \
    && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
RUN dotnet nuget add source "https://gitlab.com/api/v4/projects/35045095/packages/nuget/index.json" -u gitlab+deploy-token-925568 -p zNhLNrzsQZmpRRFXakcq --store-password-in-clear-text
COPY ["Cargo.Contract/Cargo.Contract.csproj", "Cargo.Contract/"]
COPY ["Cargo.ServiceHost/Cargo.ServiceHost.csproj", "Cargo.ServiceHost/"]
COPY ["Cargo.Application/Cargo.Application.csproj", "Cargo.Application/"]
COPY ["Cargo.Infrastructure/Cargo.Infrastructure.csproj", "Cargo.Infrastructure/"]
RUN dotnet restore "Cargo.ServiceHost/Cargo.ServiceHost.csproj"
COPY . .
WORKDIR "/src/Cargo.ServiceHost"
RUN dotnet build "Cargo.ServiceHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cargo.ServiceHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cargo.ServiceHost.dll"]
