FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src"
COPY "." "."

WORKDIR "/src/Football.API"
RUN dotnet restore && dotnet publish -c "Release" --no-restore -o "./out"

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR "/app"
EXPOSE 44367
EXPOSE 8080
COPY --from=build "/src/Football.API/out" "."
ENTRYPOINT ["dotnet", "Football.API.dll"]
