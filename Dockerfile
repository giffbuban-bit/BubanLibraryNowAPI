FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dontnet restore "BubanLibraryNowAPI/BubanLibraryNowAPI.csproj"
RUN dotnet build "BubanLibraryNowAPI/BubanLibraryNowAPI.csproj" -c Release -o /app/out

FROM base AS final
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "BubanLibraryNowAPI.dll"]
