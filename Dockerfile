ARG ConnectionStrings__Default

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app/
COPY ./*.sln ./

COPY . . 
RUN cd src; for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done; cd ../

RUN dotnet restore  

COPY ./src /app/src
WORKDIR /app/src/Theatrum.Web

RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app/

COPY --from=build-env /out .


CMD	ASPNETCORE_URLS=http://*:$PORT dotnet Theatrum.Web.dll
