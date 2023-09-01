FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["No-SQL-Project.csproj", ""]
RUN dotnet restore "No-SQL-Project.csproj"
COPY . .
RUN dotnet build "No-SQL-Project.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "No-SQL-Project.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://*:5001
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "No-SQL-Project.dll"]