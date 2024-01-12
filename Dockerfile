FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish "Presentation/CryptoApp.Presentation.csproj" -c Release -o /CryptoApp

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /CryptoApp
COPY --from=build /CryptoApp ./
EXPOSE $PORT
ENTRYPOINT ["dotnet", "CryptoApp.Presentation.dll"]