FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["DeveloperCourse.SecondLesson.Common.Clients/DeveloperCourse.SecondLesson.Common.Clients.csproj", "DeveloperCourse.SecondLesson.Common.Clients/"]
COPY ["DeveloperCourse.SecondTask.Market/DeveloperCourse.SecondTask.Market.csproj", "DeveloperCourse.SecondTask.Market/"]
COPY ["DeveloperCourse.SecondTask.Domain/DeveloperCourse.SecondTask.Domain.csproj", "DeveloperCourse.SecondTask.Domain/"]
RUN dotnet restore "DeveloperCourse.SecondTask.Market/DeveloperCourse.SecondTask.Market.csproj"
COPY . .
WORKDIR "/src/DeveloperCourse.SecondTask.Market"
RUN dotnet publish "DeveloperCourse.SecondTask.Market.csproj" -c Release -o /app 

FROM nginx:alpine
WORKDIR /var/www/web
COPY --from=build /app/wwwroot .
COPY ["DeveloperCourse.SecondTask.Market/nginx.conf", "/etc/nginx/nginx.conf"]