FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["DeveloperCourse.SecondLesson.Common.Identity/DeveloperCourse.SecondLesson.Common.Identity.csproj", "DeveloperCourse.SecondLesson.Common.Identity/"]
COPY ["DeveloperCourse.SecondLesson.Common.Web/DeveloperCourse.SecondLesson.Common.Web.csproj", "DeveloperCourse.SecondLesson.Common.Web/"]
COPY ["DeveloperCourse.SecondTask.Image.DataAccess/DeveloperCourse.SecondTask.Image.DataAccess.csproj", "DeveloperCourse.SecondTask.Image.DataAccess/"]
COPY ["DeveloperCourse.SecondTask.Image.Domain/DeveloperCourse.SecondTask.Image.Domain.csproj", "DeveloperCourse.SecondTask.Image.Domain/"]
COPY ["DeveloperCourse.SecondTask.Image.API/DeveloperCourse.SecondTask.Image.API.csproj", "DeveloperCourse.SecondTask.Image.API/"]
COPY ["DeveloperCourse.SecondTask.Domain/DeveloperCourse.SecondTask.Domain.csproj", "DeveloperCourse.SecondTask.Domain/"]
RUN dotnet restore "DeveloperCourse.SecondTask.Image.API/DeveloperCourse.SecondTask.Image.API.csproj"
COPY . .
WORKDIR "/src/DeveloperCourse.SecondTask.Image.API"
RUN dotnet publish "DeveloperCourse.SecondTask.Image.API.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "DeveloperCourse.SecondTask.Image.API.dll"]