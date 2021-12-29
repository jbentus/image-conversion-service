FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app

COPY ./src/Imagination.Server/*.csproj .
RUN dotnet restore

COPY ./src/Imagination.Server .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .

# Required for SkiaSharp
RUN apt-get update && apt-get install -y libfontconfig1

ENTRYPOINT [ "dotnet", "Imagination.Server.dll" ]