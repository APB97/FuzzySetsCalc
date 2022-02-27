#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN mkdir /data
VOLUME /data

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FuzzySetsCalc/FuzzySetsCalc.csproj", "FuzzySetsCalc/"]
RUN dotnet restore "FuzzySetsCalc/FuzzySetsCalc.csproj"
COPY . .
WORKDIR "/src/FuzzySetsCalc"
RUN dotnet build "FuzzySetsCalc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FuzzySetsCalc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FuzzySetsCalc.dll"]