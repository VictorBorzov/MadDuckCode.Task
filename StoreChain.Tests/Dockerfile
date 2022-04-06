ARG BUILD_TAG=6.0
FROM mcr.microsoft.com/dotnet/sdk:${BUILD_TAG} AS build
WORKDIR /src
COPY ["StoreChain.Tests/StoreChain.Tests.csproj", "StoreChain.Tests/"]
COPY ["StoreChain/StoreChain.csproj", "StoreChain/"]
RUN dotnet restore "StoreChain.Tests/StoreChain.Tests.csproj"
COPY . .
WORKDIR "/src/StoreChain.Tests"
RUN dotnet build "StoreChain.Tests.csproj" -c Release -o /app/build
ENTRYPOINT ["dotnet", "test"]
