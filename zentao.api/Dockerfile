FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["zentao.api/zentao.api.csproj", "zentao.api/"]
RUN dotnet restore "zentao.api/zentao.api.csproj"
COPY . .
WORKDIR "/src/zentao.api"
RUN dotnet build "zentao.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "zentao.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "zentao.api.dll"]