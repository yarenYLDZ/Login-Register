using Business.Interface;
using Business.Services;
using DataAccess.Concrete;
using DataAccess.Context;
using DataAccess.Entity;
using DataAccess.Inteface;
using DataAccess.Mapper;
using DataAccess.Security;
using DataAccess.UserValidations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
    (options =>
    {
        options.TokenValidationParameters = new
         Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            ClockSkew = TimeSpan.Zero

        };
    }
    );
        builder.Services.AddDbContext<ContextClass>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("RegisterDenemee"));
        });

        builder.Services.AddIdentityCore<User>(
            _ =>
            {
                _.Password.RequiredLength = 5; //En az kaç karakterli olmas? gerekti?ini belirtiyoruz.
                _.Password.RequireNonAlphanumeric = false; //Alfanumerik zorunlulu?unu kald?r?yoruz.
                _.Password.RequireLowercase = false; //Küçük harf zorunlulu?unu kald?r?yoruz.
                _.Password.RequireUppercase = false; //Büyük harf zorunlulu?unu kald?r?yoruz.
                _.Password.RequireDigit = false; //0-9 aras? say?sal karakter zorunlulu?unu kald?r?yoruz.

                _.User.RequireUniqueEmail = true; //Email adreslerini tekille?tiriyoruz.
                _.User.AllowedUserNameCharacters = "abcçdefghi?jklmnoöpqrs?tuüvwxyzABCÇDEFGHI?JKLMNOÖPQRS?TUÜVWXYZ0123456789-._@+"; //Kullan?c? ad?nda geçerli olan karakterleri belirtiyoruz.
            }).AddUserValidator<UserValidation>().AddPasswordValidator<PasswordValidation>().AddEntityFrameworkStores<ContextClass>(); 



        builder.Services.AddDbContext<ContextClass>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
        });

        




    

        builder.Services.AddAutoMapper(typeof(aMapper).Assembly);
        builder.Services.AddScoped<Interface1, Repository>();
        builder.Services.AddScoped<IUserServices, UserServices>();
        builder.Services.AddScoped<IJWT,tokenHandler>();
        builder.Services.AddScoped<IAdress, AdressService>();
        var app = builder.Build();
       // builder.Services.AddDefaultTokenProviders();
       // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}