FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./WhereBNB.Client.csproj", "WhereBNB.Client/"]
RUN dotnet restore "WhereBNB.Client/WhereBNB.Client.csproj"
COPY . ./WhereBNB.Client
WORKDIR "/src/WhereBNB.Client"
RUN dotnet build "WhereBNB.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WhereBNB.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "WhereBNB.Client.dll"]
