#FROM mcr.microsoft.com/dotnet/aspnet:9.0
#COPY bin/Release/net9.0/publish/ app/
#WORKDIR /app
#ENV ASPNETCORE_URLS=http://+:80
#ENTRYPOINT ["dotnet", "MarqueeManagement.HttpApi.Host.dll"]
#
#
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "src/MarqueeManagement.HttpApi.Host/MarqueeManagement.HttpApi.Host.csproj"
RUN dotnet publish "src/MarqueeManagement.HttpApi.Host/MarqueeManagement.HttpApi.Host.csproj" \
    -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "MarqueeManagement.HttpApi.Host.dll"]