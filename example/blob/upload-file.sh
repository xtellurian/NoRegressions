#!/bin/bash
DIR=$(pwd)

cd ../../src/cli/
echo
echo "Upload file apples_01.jpg to container 'apples' in Azure Blob Storage."
echo
echo dotnet run -- blob --upload --destination apples --filepath ${DIR}/img/apples/apples_01.jpg
echo
dotnet run -- blob --upload --destination apples --filepath ${DIR}/img/apples/apples_01.jpg

echo
echo "Upload file oranges_01.jpg to container 'oranges' in Azure Blob Storage."
echo
echo dotnet run -- blob --upload --destination oranges --filepath ${DIR}/img/oranges/oranges_01.jpg
echo
dotnet run -- blob --upload --destination oranges --filepath ${DIR}/img/oranges/oranges_01.jpg