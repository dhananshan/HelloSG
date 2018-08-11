FROM microsoft/aspnetcore:2.0-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1709 AS build
WORKDIR /src
COPY HelloSG/HelloSG.API.csproj HelloSG/
COPY HelloSG.Dto/HelloSG.Dto.csproj HelloSG.Dto/
COPY HelloSGService/HelloSG.Service.csproj HelloSGService/
COPY HelloSG.Config/HelloSG.Common.csproj HelloSG.Config/
RUN dotnet restore HelloSG/HelloSG.API.csproj
COPY . .
WORKDIR /src/HelloSG
RUN dotnet build HelloSG.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish HelloSG.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HelloSG.API.dll"]
