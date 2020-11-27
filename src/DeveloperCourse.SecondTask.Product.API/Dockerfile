FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["DeveloperCourse.SecondLesson.Common.Clients/DeveloperCourse.SecondLesson.Common.Clients.csproj", "DeveloperCourse.SecondLesson.Common.Clients/"]
COPY ["DeveloperCourse.SecondLesson.Common.Identity/DeveloperCourse.SecondLesson.Common.Identity.csproj", "DeveloperCourse.SecondLesson.Common.Identity/"]
COPY ["DeveloperCourse.SecondLesson.Common.Web/DeveloperCourse.SecondLesson.Common.Web.csproj", "DeveloperCourse.SecondLesson.Common.Web/"]
COPY ["DeveloperCourse.SecondTask.Product.DataAccess/DeveloperCourse.SecondTask.Product.DataAccess.csproj", "DeveloperCourse.SecondTask.Product.DataAccess/"]
COPY ["DeveloperCourse.SecondTask.Product.Domain/DeveloperCourse.SecondTask.Product.Domain.csproj", "DeveloperCourse.SecondTask.Product.Domain/"]
COPY ["DeveloperCourse.SecondTask.Product.API/DeveloperCourse.SecondTask.Product.API.csproj", "DeveloperCourse.SecondTask.Product.API/"]
COPY ["DeveloperCourse.SecondTask.Domain/DeveloperCourse.SecondTask.Domain.csproj", "DeveloperCourse.SecondTask.Domain/"]
RUN dotnet restore "DeveloperCourse.SecondTask.Product.API/DeveloperCourse.SecondTask.Product.API.csproj"
COPY . .
WORKDIR "/src/DeveloperCourse.SecondTask.Product.API"
RUN dotnet publish "DeveloperCourse.SecondTask.Product.API.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "DeveloperCourse.SecondTask.Product.API.dll"]