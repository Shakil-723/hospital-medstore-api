FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["HospitalMedstore.API/HospitalMedstore.API.csproj", "HospitalMedstore.API/"]
RUN dotnet restore "HospitalMedstore.API/HospitalMedstore.API.csproj"
COPY . .
WORKDIR "/src/HospitalMedstore.API"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HospitalMedstore.API.dll"]
