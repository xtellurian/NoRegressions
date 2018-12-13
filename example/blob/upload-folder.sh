#!/bin/bash
DIR=$(pwd)

cd ../../src/cli/
echo
echo "Upload folder apples to container 'apples' in Azure Blob Storage."
echo
echo dotnet run -- blob --upload --destination apples --filepath ${DIR}/img/apples/
echo
dotnet run -- blob --upload --destination apples --filepath ${DIR}/img/apples/

echo
echo "Upload folder oranges to container 'oranges' in Azure Blob Storage."
echo
echo dotnet run -- blob --upload --destination oranges --filepath ${DIR}/img/oranges/
echo
dotnet run -- blob --upload --destination oranges --filepath ${DIR}/img/oranges/