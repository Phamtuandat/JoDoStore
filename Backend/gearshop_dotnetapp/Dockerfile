#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["gearshop_dotnetapp/gearshop_dotnetapp.csproj", "gearshop_dotnetapp/"]
RUN dotnet restore "gearshop_dotnetapp/gearshop_dotnetapp.csproj"
COPY . .
WORKDIR "/src/gearshop_dotnetapp"
RUN dotnet build "gearshop_dotnetapp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "gearshop_dotnetapp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "gearshop_dotnetapp.dll"]