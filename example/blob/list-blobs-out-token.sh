#!/bin/bash

cd ../../src/cli/
echo
echo "Output list of blobs in container 'apples' to file 'apples.txt' with SAS tokens."
echo
echo dotnet run -- blob --list-blob apples --output ../../example/apples.txt --generate-token
echo
dotnet run -- blob --list-blob apples --output ../../example/apples.txt --generate-token

echo
echo "Output list of blobs in container 'oranges' to file 'oranges.txt' with SAS tokens."
echo
echo dotnet run -- blob --list-blob oranges --output ../../example/oranges.txt --generate-token
echo
dotnet run -- blob --list-blob oranges --output ../../example/oranges.txt --generate-token