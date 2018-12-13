#!/bin/bash

cd ../../src/cli/
echo
echo "Output list of blobs in container 'apples' to file 'apples.txt'."
echo
echo dotnet run -- blob --list-blob apples --output ../../example/apples.txt
echo
dotnet run -- blob --list-blob apples --output ../../example/apples.txt

echo
echo "Output list of blobs in container 'oranges' to file 'oranges.txt'."
echo
echo dotnet run -- blob --list-blob oranges --output ../../example/oranges.txt
echo
dotnet run -- blob --list-blob oranges --output ../../example/oranges.txt