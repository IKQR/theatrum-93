ARG ConnectionStrings__Default

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /app/
COPY ./*.sln ./

COPY . . 
RUN cd src; for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done; cd ../

RUN dotnet restore  

COPY ./src /app/src
WORKDIR /app/src/Theatrum.Web

RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app/

COPY --from=build-env /out .


CMD	ASPNETCORE_URLS=http://*:$PORT dotnet Theatrum.Web.dll