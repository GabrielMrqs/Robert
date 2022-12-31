#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RobertMQ.SenderWorker/RobertMQ.SenderWorker.csproj", "RobertMQ.SenderWorker/"]
COPY ["RobertMQ.Application/RobertMQ.Application.csproj", "RobertMQ.Application/"]
RUN dotnet restore "RobertMQ.SenderWorker/RobertMQ.SenderWorker.csproj"
COPY . .
WORKDIR "/src/RobertMQ.SenderWorker"
RUN dotnet build "RobertMQ.SenderWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RobertMQ.SenderWorker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RobertMQ.SenderWorker.dll"]