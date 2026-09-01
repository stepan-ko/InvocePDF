FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY InvocePDF.csproj .
RUN dotnet restore

COPY . .

RUN dotnet publish \
    -c Release \
    -o /app/publish \
    --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

RUN mkdir -p /app/data

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "InvocePDF.dll"]