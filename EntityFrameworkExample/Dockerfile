ARG DOTNET_TAG
ARG DB

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_TAG} AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_TAG}
WORKDIR /App
COPY --from=build-env /App/out .

#EXPOSE 5001
EXPOSE 80
ENTRYPOINT ["dotnet", "EntityFrameworkExample.dll"]
