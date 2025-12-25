using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PhotoGallery.API.Common;
using PhotoGallery.API.ErrorHandling;
using PhotoGallery.API.ErrorHandling.ExceptionMapper;
using PhotoGallery.API.Mapping;
using PhotoGallery.Domain.Album;
using PhotoGallery.Domain.Like;
using PhotoGallery.Domain.Photo;
using PhotoGallery.Domain.User;
using PhotoGallery.Infrastructure.Database;
using PhotoGallery.Infrastructure.Options;
using PhotoGallery.Infrastructure.Repositories;
using PhotoGallery.Infrastructure.Services;
using PhotoGallery.UseCases.Abstractions.Data;
using PhotoGallery.UseCases.Abstractions.Services;
using PhotoGallery.UseCases.PipelineBehaviours;
using PhotoGallery.UseCases.User.Login;


    var builder = WebApplication.CreateBuilder(args);
    
    builder.Services.AddScoped<IUserRepository, EFUserRepository>();
    builder.Services.AddScoped<IAlbumRepository, EFAlbumRepository>();
    builder.Services.AddScoped<IPhotoRepository, EFPhotoRepository>();
    builder.Services.AddScoped<ILikeRepository, EFLikeRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
    builder.Services.AddScoped<IObjectStorageService, S3ObjectStorageService>();

    builder.Services.AddProblemDetails(configure =>
    {
        configure.CustomizeProblemDetails = options =>
        {
            options.ProblemDetails.Extensions.TryAdd("traceId", System.Diagnostics.Activity.Current?.Id);
        };
    });

    builder.Services.AddSingleton<IExceptionProblemDetailsMapper, ExceptionProblemDetailsMapper>();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

    if (builder.Environment.IsDevelopment())
    {
        DotNetEnv.Env.Load("../../.env");
    }

    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssemblyContaining<LoginUserCommandHandler>();
        cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    });

    builder.Services.AddValidatorsFromAssemblyContaining<LoginUserCommandValidator>();

    builder.Services.AddAutoMapper(
        cfg => { cfg.AllowNullCollections = true; },
        new[]
        {
            typeof(LoginMappingProfile).Assembly,
            typeof(PhotoGallery.UseCases.Mapping.AlbumMappingProfile).Assembly
        }
    );

    var postgresConnection =
        $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
        $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
        $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
        $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
        $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";


    var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
    var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
    var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
    var jwtValidityMins = int.Parse((Environment.GetEnvironmentVariable("JWT_VALIDITY_MINS") ?? "120"));

    builder.Services.Configure<JwtSettings>(options =>
    {
        options.Key = jwtKey!;
        options.Issuer = jwtIssuer!;
        options.Audience = jwtAudience!;
        options.TokenValidityMins = jwtValidityMins;
    });

    builder.Services.Configure<ObjectStorageSettings>(options =>
    {
        options.Endpoint = Environment.GetEnvironmentVariable("S3_ENDPOINT") ?? string.Empty;
        options.PublicEndpoint = Environment.GetEnvironmentVariable("S3_PUBLIC_ENDPOINT") ?? string.Empty;
        options.AccessKey = Environment.GetEnvironmentVariable("S3_ACCESS_KEY") ?? string.Empty;
        options.SecretKey = Environment.GetEnvironmentVariable("S3_SECRET_KEY") ?? string.Empty;
        options.Bucket = Environment.GetEnvironmentVariable("S3_BUCKET") ?? string.Empty;
        options.Region = Environment.GetEnvironmentVariable("S3_REGION") ?? "us-east-1";
    });

    builder.Services.AddDbContextFactory<AppDbContext>(options =>
        options.UseNpgsql(postgresConnection));

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend", policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS", "PATCH")
                .WithHeaders("Content-Type", "Authorization")
                .AllowCredentials();
        });
    });

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });
    builder.Services.AddAuthorization();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddEndpointsApiExplorer();
    
    builder.Services.AddEndpoints(typeof(Program).Assembly);
    
    builder.Services.AddSwaggerGen(options =>
    {
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            BearerFormat = "JWT",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Description = "Enter your JWT access token",
            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };

        options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
        options.EnableAnnotations();
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { jwtSecurityScheme, Array.Empty<string>() }
        });
    });

    var app = builder.Build();
    
    app.UseExceptionHandler();
    app.UseCors("AllowFrontend");
    app.UseAuthentication();
    app.UseAuthorization();
    
    var prefix = app.MapGroup("/api/v1");
    app.MapEndpoints(prefix);

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.Run();
    
