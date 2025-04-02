using CloudinaryDotNet;
using FSA_3S.Helpers;
using FSA_3S.Models;
using FSA_3S.Repositories.Interface;
using FSA_3S.Repositories.Repository;
using FSA_3S.Services.Interface;
using FSA_3S.Services.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// 2. Cấu hình Database
var connectionString = builder.Configuration.GetConnectionString("Db");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// ✅ 3. Đăng ký Controllers (Sửa lỗi)
builder.Services.AddControllers(); // <-- Di chuyển dòng này lên trên

// 4. Cấu hình Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<IPersonalInformationService, PersonalInformationService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IRealEstateService, RealEstateService>();
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IWorkService, WorkService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IMappingUserAppointmentService, MappingUserAppointmentService>();

// 5. Cấu hình Repositories
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IRealEstateRepository, RealEstateRepository>();
builder.Services.AddScoped<IWorkRepository, WorkRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IMappingUserAppointmentRepository, MappingUserAppointmentRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

// 6. Cấu hình JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// 7. Cấu hình Cloudinary
builder.Services.AddSingleton(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var account = new Account(
        config["Cloudinary:CloudName"],
        config["Cloudinary:ApiKey"],
        config["Cloudinary:ApiSecret"]
    );
    return new Cloudinary(account);
});

// 8. Cấu hình SignalR ✅ (Không đăng ký `NotificationHub` ở đây)
builder.Services.AddSignalR();
builder.Services.AddSingleton<NotificationHub>();


// 9. Cấu hình Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập 'Bearer' [space] và token vào đây. Ví dụ: Bearer abc123"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// 10. Chạy Migration nếu cần
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Migration Error: {ex.Message}");
    }
}

// 11. Cấu hình Middleware
app.UseHttpsRedirection();
app.UseCors("AllowReactApp");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// 12. Định tuyến API và SignalR
app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");

// 13. Cấu hình Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();