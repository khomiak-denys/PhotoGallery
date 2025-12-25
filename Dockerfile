FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build 
WORKDIR /app 


COPY *.sln . 
COPY src/PhotoGallery.API/*csproj src/PhotoGallery.API/
COPY src/PhotoGallery.UseCases/*csproj src/PhotoGallery.UseCases/
COPY src/PhotoGallery.Infrastructure/*csproj src/PhotoGallery.Infrastructure/
COPY src/PhotoGallery.Domain/*csproj src/PhotoGallery.Domain/

RUN dotnet restore

RUN dotnet tool install --global dotnet-ef --version 8.*
ENV PATH="$PATH:/root/.dotnet/tools"

COPY . .

RUN dotnet publish src/PhotoGallery.API/*csproj -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app 

COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "PhotoGallery.API.dll"]

FROM build AS migrator_image
WORKDIR /app
COPY --from=build /app/publish .