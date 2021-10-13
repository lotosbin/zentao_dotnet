FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["xtask.api/xtask.api.csproj", "xtask.api/"]
RUN dotnet restore "xtask.api/xtask.api.csproj"
COPY . .
WORKDIR "/src/xtask.api"
RUN dotnet build "xtask.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "xtask.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "xtask.api.dll"]
