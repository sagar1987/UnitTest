#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG MICROSERVICE=''
WORKDIR /src
COPY ["Lakeshore.Service/Lakeshore.Service.csproj", "Lakeshore.Service/"]
RUN dotnet restore -s "https://hq-artifactorypro.llmhq.com/artifactory/api/nuget/v3/nuget-local" -s "https://api.nuget.org/v3/index.json" "Lakeshore.Service/Lakeshore.Service.csproj"
RUN dotnet restore "Lakeshore.Service/Lakeshore.Service.csproj"
COPY . .
WORKDIR "/src/Lakeshore.Service"
RUN dotnet build "Lakeshore.Service.csproj" -c Release -o /app/build
FROM build AS publish
ARG MICROSERVICE=''
RUN dotnet publish "Lakeshore.Service.csproj" -c Release -o /app/publish
FROM base AS final
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
ARG MICROSERVICE=''
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lakeshore.Service.dll"]