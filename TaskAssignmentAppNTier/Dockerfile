#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TaskAssignmentAppNTier/TaskAssignmentAppNTier.csproj", "TaskAssignmentAppNTier/"]
COPY ["TaskAssigmentApp.Domain/TaskAssigmentApp.Domain.csproj", "TaskAssigmentApp.Domain/"]
COPY ["TaskAssignmentApp.Application/TaskAssignmentApp.Application.csproj", "TaskAssignmentApp.Application/"]
COPY ["TaskAssignmentApp.Infrastructure/TaskAssignmentApp.Infrastructure.csproj", "TaskAssignmentApp.Infrastructure/"]
COPY ["TaskAssignmentApp.Persistance/TaskAssignmentApp.Persistance.csproj", "TaskAssignmentApp.Persistance/"]
RUN dotnet restore "TaskAssignmentAppNTier/TaskAssignmentAppNTier.csproj"
COPY . .
WORKDIR "/src/TaskAssignmentAppNTier"
RUN dotnet build "TaskAssignmentAppNTier.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskAssignmentAppNTier.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskAssignmentAppNTier.dll"]