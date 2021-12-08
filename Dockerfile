ARG ConnectionStrings__Default

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app/
COPY ./*.sln ./

COPY . . 
RUN dotnet restore  

COPY ./src /app/src
WORKDIR /app/src/Theatrum.Web.Razor

RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app/

COPY --from=build-env /out .


CMD	ASPNETCORE_URLS=http://*:$PORT dotnet Theatrum.Web.Razor.dll
