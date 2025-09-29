# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SocialSecurityInstitution.PresentationLayer/SocialSecurityInstitution.PresentationLayer.csproj", "SocialSecurityInstitution.PresentationLayer/"]
COPY ["SocialSecurityInstitution.BusinessObjectLayer/SocialSecurityInstitution.BusinessObjectLayer.csproj", "SocialSecurityInstitution.BusinessObjectLayer/"]
COPY ["SocialSecurityInstitution.BusinessLogicLayer/SocialSecurityInstitution.BusinessLogicLayer.csproj", "SocialSecurityInstitution.BusinessLogicLayer/"]
COPY ["SocialSecurityInstitution.DataAccessLayer/SocialSecurityInstitution.DataAccessLayer.csproj", "SocialSecurityInstitution.DataAccessLayer/"]
RUN dotnet restore "./SocialSecurityInstitution.PresentationLayer/SocialSecurityInstitution.PresentationLayer.csproj"
COPY . .
WORKDIR "/src/SocialSecurityInstitution.PresentationLayer"
RUN dotnet build "./SocialSecurityInstitution.PresentationLayer.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SocialSecurityInstitution.PresentationLayer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialSecurityInstitution.PresentationLayer.dll"]