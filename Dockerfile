FROM mcr.microsoft.com/dotnet/sdk:10.0 AS restore
WORKDIR /app
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/devHabit.Api/devHabit.Api.csproj", "src/devHabit.Api/"]
RUN dotnet restore "src/devHabit.Api/devHabit.Api.csproj"

FROM restore AS build
WORKDIR /app
COPY . .
RUN dotnet build "src/devHabit.Api/devHabit.Api.csproj" -c Release -o ./build

FROM build AS publish
RUN dotnet publish "src/devHabit.Api/devHabit.Api.csproj" -c Release -o ./publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
EXPOSE 8080
USER $APP_UID
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "devHabit.Api.dll"]
