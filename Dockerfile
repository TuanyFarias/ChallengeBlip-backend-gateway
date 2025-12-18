FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src


COPY ["ChallengeBlip.csproj", "./"]
RUN dotnet restore "ChallengeBlip.csproj"


COPY . .

RUN rm -rf tests/
RUN dotnet publish "ChallengeBlip.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ChallengeBlip.dll"]