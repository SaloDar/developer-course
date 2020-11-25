FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["DeveloperCourse.SecondLesson.Common.Identity/DeveloperCourse.SecondLesson.Common.Identity.csproj", "DeveloperCourse.SecondLesson.Common.Identity/"]
COPY ["DeveloperCourse.SecondLesson.Common.Web/DeveloperCourse.SecondLesson.Common.Web.csproj", "DeveloperCourse.SecondLesson.Common.Web/"]
COPY ["DeveloperCourse.SecondTask.Price.DataAccess/DeveloperCourse.SecondTask.Price.DataAccess.csproj", "DeveloperCourse.SecondTask.Price.DataAccess/"]
COPY ["DeveloperCourse.SecondTask.Price.Domain/DeveloperCourse.SecondTask.Price.Domain.csproj", "DeveloperCourse.SecondTask.Price.Domain/"]
COPY ["DeveloperCourse.SecondTask.Price.API/DeveloperCourse.SecondTask.Price.API.csproj", "DeveloperCourse.SecondTask.Price.API/"]
COPY ["DeveloperCourse.SecondTask.Domain/DeveloperCourse.SecondTask.Domain.csproj", "DeveloperCourse.SecondTask.Domain/"]
RUN dotnet restore "DeveloperCourse.SecondTask.Price.API/DeveloperCourse.SecondTask.Price.API.csproj"
COPY . .
WORKDIR "/src/DeveloperCourse.SecondTask.Price.API"
RUN dotnet publish "DeveloperCourse.SecondTask.Price.API.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "DeveloperCourse.SecondTask.Price.API.dll"]