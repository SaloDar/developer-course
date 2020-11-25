FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["DeveloperCourse.SecondLesson.Common.Identity/DeveloperCourse.SecondLesson.Common.Identity.csproj", "DeveloperCourse.SecondLesson.Common.Identity/"]
COPY ["DeveloperCourse.SecondLesson.Common.Web/DeveloperCourse.SecondLesson.Common.Web.csproj", "DeveloperCourse.SecondLesson.Common.Web/"]
COPY ["DeveloperCourse.SecondTask.Identity.DataAccess/DeveloperCourse.SecondTask.Identity.DataAccess.csproj", "DeveloperCourse.SecondTask.Identity.DataAccess/"]
COPY ["DeveloperCourse.SecondTask.Identity.Domain/DeveloperCourse.SecondTask.Identity.Domain.csproj", "DeveloperCourse.SecondTask.Identity.Domain/"]
COPY ["DeveloperCourse.SecondTask.Identity.API/DeveloperCourse.SecondTask.Identity.API.csproj", "DeveloperCourse.SecondTask.Identity.API/"]
COPY ["DeveloperCourse.SecondTask.Domain/DeveloperCourse.SecondTask.Domain.csproj", "DeveloperCourse.SecondTask.Domain/"]
RUN dotnet restore "DeveloperCourse.SecondTask.Identity.API/DeveloperCourse.SecondTask.Identity.API.csproj"
COPY . .
WORKDIR "/src/DeveloperCourse.SecondTask.Identity.API"
RUN dotnet publish "DeveloperCourse.SecondTask.Identity.API.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "DeveloperCourse.SecondTask.Identity.API.dll"]