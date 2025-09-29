# Teknolojiler ve Bağımlılıklar

## 🔧 Ana Teknoloji Stack'i

### Backend Teknolojileri
- **.NET Core 6.0+**: Ana framework
- **ASP.NET Core MVC**: Web application framework
- **Entity Framework Core**: Object-Relational Mapping (ORM)
- **AutoMapper**: Object-to-object mapping
- **SignalR**: Real-time web functionality
- **NToastNotify**: Toast notification system

### Frontend Teknolojileri
- **HTML5**: Markup language
- **CSS3**: Styling
- **JavaScript (ES6+)**: Client-side scripting
- **Bootstrap**: CSS framework
- **jQuery**: JavaScript library
- **Font Awesome**: Icon library

### Veritabanı
- **SQL Server**: Primary database
- **Entity Framework Migrations**: Database versioning

### DevOps ve Deployment
- **Docker**: Containerization
- **Docker Compose**: Multi-container orchestration
- **Git**: Version control
- **GitHub**: Repository hosting

## 📦 NuGet Paketleri

### Core Paketler
```xml
<PackageReference Include="Microsoft.AspNetCore.App" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="6.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="6.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="6.0.0" />
```

### Mapping ve Validation
```xml
<PackageReference Include="AutoMapper" Version="12.0.0" />
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.0" />
<PackageReference Include="FluentValidation" Version="11.0.0" />
<PackageReference Include="FluentValidation.AspNetCore" Version="11.0.0" />
```

### UI ve Notification
```xml
<PackageReference Include="NToastNotify" Version="8.0.0" />
<PackageReference Include="Microsoft.AspNetCore.SignalR" Version="1.1.0" />
```

### Authentication ve Authorization
```xml
<PackageReference Include="Microsoft.AspNetCore.Authentication.Cookies" Version="2.2.0" />
<PackageReference Include="Microsoft.AspNetCore.Authorization" Version="6.0.0" />
```

## 🏗️ Proje Bağımlılık Hiyerarşisi

```
SocialSecurityInstitution.PresentationLayer
├── SocialSecurityInstitution.BusinessLogicLayer
│   ├── SocialSecurityInstitution.BusinessObjectLayer
│   └── SocialSecurityInstitution.DataAccessLayer
│       └── SocialSecurityInstitution.BusinessObjectLayer
└── SocialSecurityInstitution.BusinessObjectLayer
```

### Katman Bağımlılıkları

#### PresentationLayer Dependencies:
- BusinessLogicLayer (Direct)
- BusinessObjectLayer (Direct - for DTOs)

#### BusinessLogicLayer Dependencies:
- DataAccessLayer (Direct)
- BusinessObjectLayer (Direct - for DTOs and Entities)

#### DataAccessLayer Dependencies:
- BusinessObjectLayer (Direct - for Entities)

#### BusinessObjectLayer Dependencies:
- None (Base layer)

## 🔧 Configuration Files

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SocialSecurityDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "SignalR": {
    "EnableDetailedErrors": true
  }
}
```

### libman.json
```json
{
  "version": "1.0",
  "defaultProvider": "cdnjs",
  "libraries": [
    {
      "library": "bootstrap@5.1.3",
      "destination": "wwwroot/lib/bootstrap/"
    },
    {
      "library": "jquery@3.6.0",
      "destination": "wwwroot/lib/jquery/"
    },
    {
      "library": "font-awesome@6.0.0",
      "destination": "wwwroot/lib/font-awesome/"
    }
  ]
}
```

## 🐳 Docker Configuration

### Dockerfile
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SocialSecurityInstitution.PresentationLayer/SocialSecurityInstitution.PresentationLayer.csproj", "SocialSecurityInstitution.PresentationLayer/"]
COPY ["SocialSecurityInstitution.BusinessLogicLayer/SocialSecurityInstitution.BusinessLogicLayer.csproj", "SocialSecurityInstitution.BusinessLogicLayer/"]
COPY ["SocialSecurityInstitution.DataAccessLayer/SocialSecurityInstitution.DataAccessLayer.csproj", "SocialSecurityInstitution.DataAccessLayer/"]
COPY ["SocialSecurityInstitution.BusinessObjectLayer/SocialSecurityInstitution.BusinessObjectLayer.csproj", "SocialSecurityInstitution.BusinessObjectLayer/"]

RUN dotnet restore "SocialSecurityInstitution.PresentationLayer/SocialSecurityInstitution.PresentationLayer.csproj"
COPY . .
WORKDIR "/src/SocialSecurityInstitution.PresentationLayer"
RUN dotnet build "SocialSecurityInstitution.PresentationLayer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SocialSecurityInstitution.PresentationLayer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialSecurityInstitution.PresentationLayer.dll"]
```

### docker-compose.yml
```yaml
version: '3.8'

services:
  socialSecurityApp:
    build: .
    ports:
      - "8080:80"
    depends_on:
      - sqlserver
    environment:
      - ConnectionStrings__DefaultConnection=Server=sqlserver;Database=SocialSecurityDB;User Id=sa;Password=YourPassword123;

  sqlserver:
    image: mcr.microsoft.com/mssql/server:2019-latest
    environment:
      SA_PASSWORD: "YourPassword123"
      ACCEPT_EULA: "Y"
    ports:
      - "1433:1433"
```

## 🔐 Güvenlik Konfigürasyonları

### Authentication Setup
```csharp
services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });
```

### Authorization Policies
```csharp
services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("PersonelYonetimi", policy => policy.RequireClaim("Permission", "PersonelYonetimi"));
    options.AddPolicy("KanalYonetimi", policy => policy.RequireClaim("Permission", "KanalYonetimi"));
    options.AddPolicy("BankoYonetimi", policy => policy.RequireClaim("Permission", "BankoYonetimi"));
});
```

## 📊 Performans Optimizasyonları

### Entity Framework Optimizations
```csharp
services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
        sqlOptions.CommandTimeout(30);
    });
    options.EnableSensitiveDataLogging(false);
    options.EnableServiceProviderCaching();
    options.EnableDetailedErrors(false);
});
```

### AutoMapper Configuration
```csharp
services.AddAutoMapper(typeof(AutoMapperProfile));
```

### SignalR Configuration
```csharp
services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.KeepAliveInterval = TimeSpan.FromSeconds(15);
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
});
```

## 🔄 Middleware Pipeline

### Program.cs Middleware Order
```csharp
var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseNToastNotify();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<BankoHub>("/bankoHub");
app.MapHub<TvHub>("/tvHub");

app.Run();
```

## 🧪 Test Framework'leri

### Unit Testing
```xml
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.0.0" />
<PackageReference Include="xunit" Version="2.4.1" />
<PackageReference Include="xunit.runner.visualstudio" Version="2.4.3" />
<PackageReference Include="Moq" Version="4.16.1" />
<PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="6.0.0" />
```

### Integration Testing
```xml
<PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="6.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="6.0.0" />
```

## 📝 Logging Configuration

### Serilog Setup
```xml
<PackageReference Include="Serilog.AspNetCore" Version="5.0.0" />
<PackageReference Include="Serilog.Sinks.File" Version="5.0.0" />
<PackageReference Include="Serilog.Sinks.Console" Version="4.0.1" />
```

```csharp
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
```

## 🔧 Development Tools

### IDE ve Editörler
- **Visual Studio 2022**: Primary IDE
- **Visual Studio Code**: Alternative editor
- **SQL Server Management Studio**: Database management

### Package Managers
- **NuGet**: .NET package management
- **LibMan**: Client-side library management
- **npm**: Node.js package management (if needed)

### Version Control
- **Git**: Distributed version control
- **GitHub**: Repository hosting
- **GitKraken**: Git GUI client

## 🚀 Deployment Stratejileri

### Development Environment
- **IIS Express**: Local development server
- **SQL Server LocalDB**: Local database
- **Hot Reload**: Real-time code changes

### Production Environment
- **IIS**: Web server
- **SQL Server**: Production database
- **Docker**: Containerized deployment
- **Azure App Service**: Cloud hosting option

Bu teknoloji stack'i, **modern**, **ölçeklenebilir** ve **maintainable** bir enterprise uygulaması için gerekli tüm bileşenleri içermektedir.
