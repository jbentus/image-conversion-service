FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY ./src/Imagination.Server/*.csproj ./src/Imagination.Server/
RUN dotnet restore ./src/Imagination.Server

COPY ./tests/Imagination.Server.Benchmark/*.csproj ./tests/Imagination.Server.Benchmark/
RUN dotnet restore ./tests/Imagination.Server.Benchmark

COPY ./src/Imagination.Server ./src/Imagination.Server
COPY ./tests/Imagination.Server.Benchmark ./tests/Imagination.Server.Benchmark
COPY ./resources ./resources

WORKDIR /app/tests/Imagination.Server.Benchmark
RUN dotnet publish -c Release -o /app

FROM build AS final
RUN apt-get update && apt-get install -y libfontconfig1
WORKDIR /app
ENTRYPOINT [ "dotnet", "Imagination.Server.Benchmark.dll" ]