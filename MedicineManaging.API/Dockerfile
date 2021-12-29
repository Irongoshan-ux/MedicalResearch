#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MedicineManaging.API/MedicineManaging.API.csproj", "MedicineManaging.API/"]
COPY ["MedicineManaging.Infrastructure/MedicineManaging.Infrastructure.csproj", "MedicineManaging.Infrastructure/"]
COPY ["MedicineManaging.Domain/MedicineManaging.Domain.csproj", "MedicineManaging.Domain/"]
RUN dotnet restore "MedicineManaging.API/MedicineManaging.API.csproj"
COPY . .
WORKDIR "/src/MedicineManaging.API"
RUN dotnet build "MedicineManaging.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MedicineManaging.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MedicineManaging.API.dll"]
