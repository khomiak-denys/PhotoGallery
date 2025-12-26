#### Setup
-	Clone repository 
-	Run dotnet restore inside PhotoGallery.API
-  Configure environment variables using .env file   
-	Run docker build -t photogallery_api:latest . --target runtime – to build aspnet:8.0 image for api.
-	Run docker build -t photogallery_api:migrator . --target migrator_image – to build dotnet/sdk:8.0 for database migrator.
-	Run docker compose up -d – to start application.
-	Run docker compose --profile migration_tools run --rm migrator – to apply migrations.

### Example of .env file  
**-------- PostgreSQL --------**  
DB_USER=postgres  
DB_PASSWORD=121425  
DB_NAME=PhotoGalleryDb  
DB_HOST=db  
DB_PORT=5432  
** ---------- Object Storage (MinIO/S3) ----------**
OBJECT_STORAGE_USER=minioadmin
OBJECT_STORAGE_PASSWORD=minioadmin
OBJECT_STORAGE_BUCKET=photogallery
S3_ENDPOINT=http://minio:9000
S3_PUBLIC_ENDPOINT=http://localhost:9000
S3_REGION=us-east-1
**---------- JWT ----------**  
JWT_KEY=dfe7a23fefeea519e9bbfdd1a6be94c4b2e4529dd6b7cbea83f9959c2621b13c  
JWT_ISSUER=PhotoGallery.API  
JWT_AUDIENCE=PhotoGallery.Client  
JWT_VALIDITY_MINS=30  
