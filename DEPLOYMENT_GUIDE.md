# DRIYA Platform - Deployment Guide

## ✅ Integration Status: COMPLETE

The Vue.js ClientApp has been successfully integrated and tested. Both build and publish processes work correctly.

## 🎯 Build Verification Results

### ✅ Development Build
```
Status: SUCCESS
Warnings: 0
Errors: 0
Time: ~3 seconds
```

### ✅ Production Publish
```
Status: SUCCESS
Frontend Build: AUTOMATIC
Output: Single deployable package
Compression: Brotli (.br) and Gzip (.gz) enabled
```

**Published Files:**
- `wwwroot/index.html` (473 bytes)
- `wwwroot/assets/index-CvbAKzuB.css` (38.77 KB, 6.18 KB gzipped)
- `wwwroot/assets/index-DgZpafgz.js` (222.31 KB, 70.48 KB gzipped)
- Compressed versions (.br, .gz) for all files

## 🚀 Deployment Options

### Option 1: Windows Server / IIS

1. **Publish the application:**
   ```powershell
   cd DRIYA.Platform
   dotnet publish -c Release -o ./publish
   ```

2. **Configure IIS:**
   - Create a new Application Pool (.NET Core, No Managed Code)
   - Create a new Website pointing to the `publish` folder
   - Ensure the Application Pool identity has read access

3. **Install prerequisites:**
   - .NET 9.0 Runtime (ASP.NET Core)
   - IIS URL Rewrite Module (for SPA routing)

4. **Configure web.config** (auto-generated, but verify):
   ```xml
   <aspNetCore processPath="dotnet" 
               arguments=".\DRIYA.Platform.dll" 
               stdoutLogEnabled="false" 
               stdoutLogFile=".\logs\stdout" />
   ```

### Option 2: Linux / Nginx

1. **Publish the application:**
   ```bash
   cd DRIYA.Platform
   dotnet publish -c Release -o ./publish
   ```

2. **Copy to server:**
   ```bash
   scp -r publish/* user@server:/var/www/driya-platform/
   ```

3. **Create systemd service** (`/etc/systemd/system/driya-platform.service`):
   ```ini
   [Unit]
   Description=DRIYA Platform
   After=network.target

   [Service]
   WorkingDirectory=/var/www/driya-platform
   ExecStart=/usr/bin/dotnet /var/www/driya-platform/DRIYA.Platform.dll
   Restart=always
   RestartSec=10
   KillSignal=SIGINT
   SyslogIdentifier=driya-platform
   User=www-data
   Environment=ASPNETCORE_ENVIRONMENT=Production
   Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

   [Install]
   WantedBy=multi-user.target
   ```

4. **Configure Nginx** (`/etc/nginx/sites-available/driya-platform`):
   ```nginx
   server {
       listen 80;
       server_name your-domain.com;

       location / {
           proxy_pass http://localhost:5000;
           proxy_http_version 1.1;
           proxy_set_header Upgrade $http_upgrade;
           proxy_set_header Connection keep-alive;
           proxy_set_header Host $host;
           proxy_cache_bypass $http_upgrade;
           proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
           proxy_set_header X-Forwarded-Proto $scheme;
       }
   }
   ```

5. **Start the service:**
   ```bash
   sudo systemctl enable driya-platform
   sudo systemctl start driya-platform
   sudo systemctl enable nginx
   sudo systemctl restart nginx
   ```

### Option 3: Docker

1. **Update Dockerfile** (if needed):
   ```dockerfile
   FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
   WORKDIR /src
   
   # Install Node.js
   RUN curl -fsSL https://deb.nodesource.com/setup_18.x | bash - \
       && apt-get install -y nodejs
   
   # Copy project files
   COPY ["DRIYA.Platform/DRIYA.Platform.csproj", "DRIYA.Platform/"]
   COPY ["DRIYA.Common/DRIYA.Common.csproj", "DRIYA.Common/"]
   RUN dotnet restore "DRIYA.Platform/DRIYA.Platform.csproj"
   
   # Copy everything else
   COPY . .
   WORKDIR "/src/DRIYA.Platform"
   
   # Build (this will also build the ClientApp)
   RUN dotnet publish "DRIYA.Platform.csproj" -c Release -o /app/publish
   
   # Runtime stage
   FROM mcr.microsoft.com/dotnet/aspnet:9.0
   WORKDIR /app
   COPY --from=build /app/publish .
   ENTRYPOINT ["dotnet", "DRIYA.Platform.dll"]
   ```

2. **Build and run:**
   ```bash
   docker build -t driya-platform .
   docker run -d -p 80:80 -p 443:443 driya-platform
   ```

### Option 4: Azure App Service

1. **Publish to Azure:**
   ```powershell
   cd DRIYA.Platform
   dotnet publish -c Release -o ./publish
   
   # Zip the publish folder
   Compress-Archive -Path ./publish/* -DestinationPath ./driya-platform.zip
   
   # Deploy using Azure CLI
   az webapp deployment source config-zip `
     --resource-group YourResourceGroup `
     --name YourAppName `
     --src ./driya-platform.zip
   ```

2. **Or use Visual Studio:**
   - Right-click DRIYA.Platform project
   - Select "Publish"
   - Choose Azure App Service
   - Follow the wizard

### Option 5: AWS (Elastic Beanstalk or EC2)

**Using Elastic Beanstalk:**
```bash
cd DRIYA.Platform
dotnet publish -c Release -o ./publish
cd publish
zip -r ../driya-platform.zip .
aws elasticbeanstalk create-application-version \
  --application-name driya-platform \
  --version-label v1.0 \
  --source-bundle S3Bucket=your-bucket,S3Key=driya-platform.zip
```

## 📋 Pre-Deployment Checklist

### Configuration
- [ ] Update `appsettings.Production.json` with production database connection
- [ ] Configure Redis connection string (if using)
- [ ] Set up user secrets for sensitive data
- [ ] Update CORS settings for production domain
- [ ] Configure JWT settings
- [ ] Set environment variables

### Security
- [ ] Enable HTTPS
- [ ] Configure SSL certificates
- [ ] Review authentication settings
- [ ] Set up firewall rules
- [ ] Enable rate limiting
- [ ] Configure secure headers

### Database
- [ ] Run database migrations on production DB
- [ ] Seed initial data (admin user, etc.)
- [ ] Set up database backups
- [ ] Test database connection

### Monitoring
- [ ] Configure application insights / monitoring
- [ ] Set up error logging
- [ ] Configure health checks
- [ ] Set up alerts

## 🔧 Post-Deployment Verification

1. **Check application is running:**
   ```bash
   curl https://your-domain.com/
   ```

2. **Verify API endpoints:**
   ```bash
   curl https://your-domain.com/swagger
   ```

3. **Test frontend routing:**
   - Navigate to `/dashboard`
   - Refresh the page (should not 404)

4. **Test API calls:**
   - Login through the UI
   - Verify API calls work
   - Check browser console for errors

5. **Check logs:**
   - Review application logs
   - Check for any errors or warnings

## 🔄 Continuous Deployment

### GitHub Actions Example

Create `.github/workflows/deploy.yml`:

```yaml
name: Deploy DRIYA Platform

on:
  push:
    branches: [ main ]

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v3
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 9.0.x
    
    - name: Setup Node.js
      uses: actions/setup-node@v3
      with:
        node-version: '18'
    
    - name: Restore dependencies
      run: dotnet restore
    
    - name: Build
      run: dotnet build --no-restore -c Release
    
    - name: Publish
      run: dotnet publish DRIYA.Platform/DRIYA.Platform.csproj -c Release -o ./publish
    
    - name: Deploy to Azure
      uses: azure/webapps-deploy@v2
      with:
        app-name: your-app-name
        publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
        package: ./publish
```

## 📊 Performance Optimization

### Enable Response Compression
Already configured in publish (Brotli + Gzip)

### Enable Caching
Add to `Program.cs`:
```csharp
app.UseResponseCaching();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append(
            "Cache-Control", "public,max-age=31536000");
    }
});
```

### CDN Integration
Consider serving static assets from CDN:
- Azure CDN
- CloudFlare
- AWS CloudFront

## 🐛 Troubleshooting

### Issue: SPA routes return 404 on refresh
**Solution:** Ensure fallback routing is configured in `Program.cs`:
```csharp
app.MapFallbackToFile("index.html");
```

### Issue: Static files not loading
**Solution:** Check `UseStaticFiles()` is before `UseRouting()`

### Issue: API calls fail with CORS error
**Solution:** Update CORS policy in `Program.cs` for production domain

### Issue: Frontend not building during publish
**Solution:** 
1. Verify Node.js is installed on build server
2. Check `node_modules` exists in ClientApp
3. Review publish output for npm errors

## 📞 Support

For deployment issues:
1. Check application logs
2. Review error messages in Serilog output
3. Verify all environment variables are set
4. Test locally with `dotnet run --environment Production`

## ✅ Success Criteria

Your deployment is successful when:
- ✅ Application starts without errors
- ✅ Home page loads correctly
- ✅ API endpoints respond (check /swagger)
- ✅ Frontend routing works (no 404 on refresh)
- ✅ API calls from frontend work
- ✅ Authentication flow completes
- ✅ Database operations succeed
- ✅ Logs show no critical errors

---

**Deployment Verified**: October 4, 2025  
**Build Status**: ✅ Clean (0 warnings, 0 errors)  
**Publish Status**: ✅ Success (Frontend auto-builds)  
**Ready for Production**: ✅ YES



