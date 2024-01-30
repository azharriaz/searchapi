#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#ENV ASPNETCORE_URLS=https://+:5006;http://+:5005
WORKDIR /app
EXPOSE 80
EXPOSE 433

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Apps/CodingChallenge.Api/CodingChallenge.Api.csproj", "src/Apps/CodingChallenge.Api/"]
COPY ["src/Common/CodingChallenge.Infrastructure/CodingChallenge.Infrastructure.csproj", "src/Common/CodingChallenge.Api.Infrastructure/"]
COPY ["src/Common/CodingChallenge.Application/CodingChallenge.Application.csproj", "src/Common/CodingChallenge.Api.Application/"]
COPY ["src/Common/CodingChallenge.Domain/CodingChallenge.Domain.csproj", "src/Common/CodingChallenge.Api.Domain/"]
RUN dotnet restore "src/Apps/CodingChallenge.Api/CodingChallenge.Api.csproj"
COPY . .
WORKDIR "/src/src/Apps/CodingChallenge.Api"
RUN dotnet build "CodingChallenge.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CodingChallenge.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CodingChallenge.Api.dll"]