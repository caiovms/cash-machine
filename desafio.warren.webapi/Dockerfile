FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["desafio.warren.webapi/desafio.warren.webapi.csproj", "desafio.warren.webapi/"]
COPY ["desafio.warren.ioc/desafio.warren.ioc.csproj", "desafio.warren.ioc/"]
COPY ["desafio.warren.cross.cutting/desafio.warren.cross.cutting.csproj", "desafio.warren.cross.cutting/"]
COPY ["desafio.warren.dominio/desafio.warren.domain.csproj", "desafio.warren.dominio/"]
COPY ["desafio.warren.application.dto/desafio.warren.application.dto.csproj", "desafio.warren.application.dto/"]
COPY ["desafio.warren.repository/desafio.warren.repository.csproj", "desafio.warren.repository/"]
COPY ["desafio.warren.domain.core/desafio.warren.domain.core.csproj", "desafio.warren.domain.core/"]
COPY ["desafio.warren.data/desafio.warren.data.csproj", "desafio.warren.data/"]
COPY ["desafio.warren.services/desafio.warren.services.csproj", "desafio.warren.services/"]
COPY ["desafio.warren.application/desafio.warren.application.csproj", "desafio.warren.application/"]
RUN dotnet restore "desafio.warren.webapi/desafio.warren.webapi.csproj"
COPY . .
WORKDIR "/src/desafio.warren.webapi"
RUN dotnet build "desafio.warren.webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "desafio.warren.webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "desafio.warren.webapi.dll"]