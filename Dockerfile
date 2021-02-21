#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["WrongMove/WrongMove.csproj", "WrongMove/"]
RUN dotnet restore "WrongMove/WrongMove.csproj"
COPY . .
WORKDIR "/src/WrongMove"
RUN dotnet build "WrongMove.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WrongMove.csproj" -c Release -o /app/publish --self-contained false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WrongMove.dll"]