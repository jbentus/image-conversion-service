FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Required for SkiaSharp
RUN apt-get update && apt-get install -y libfontconfig1

COPY ./resources ./resources
COPY ./tests/Imagination.Server.Benchmark/*.csproj ./tests/Imagination.Server.Benchmark/
RUN dotnet restore ./tests/Imagination.Server.Benchmark

WORKDIR /app/tests/Imagination.Server.Benchmark
COPY ./tests/Imagination.Server.Benchmark ./
RUN dotnet publish -c Release -o /app

WORKDIR /app
ENTRYPOINT [ "dotnet", "Imagination.Server.Benchmark.dll" ]