FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build

ARG BUILDCONFIG=RELEASE
ARG VERSION=1.0.0

COPY SampleRestWebApi.csproj /build/

RUN dotnet restore ./build/SampleRestWebApi.csproj

COPY . ./build/
WORKDIR /build/
RUN dotnet publish ./SampleRestWebApi.csproj -c $BUILDCONFIG -o out /p:Version=$VERSION

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app

COPY --from=build /build/out .

ENTRYPOINT ["dotnet", "SampleRestWebApi.dll"]