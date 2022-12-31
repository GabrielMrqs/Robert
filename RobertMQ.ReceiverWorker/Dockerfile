#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RobertMQ.ReceiverWorker/RobertMQ.ReceiverWorker.csproj", "RobertMQ.ReceiverWorker/"]
COPY ["RobertMQ.Application/RobertMQ.Application.csproj", "RobertMQ.Application/"]
RUN dotnet restore "RobertMQ.ReceiverWorker/RobertMQ.ReceiverWorker.csproj"
COPY . .
WORKDIR "/src/RobertMQ.ReceiverWorker"
RUN dotnet build "RobertMQ.ReceiverWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RobertMQ.ReceiverWorker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RobertMQ.ReceiverWorker.dll"]