#!/bin/bash

cd ../../src/cli/
echo
echo "List blobs in container 'apples'."
echo
echo dotnet run -- blob --list-blob apples
echo
dotnet run -- blob --list-blob apples

echo
echo "List blobs in container 'oranges'."
echo
echo dotnet run -- blob --list-blob oranges
echo
dotnet run -- blob --list-blob oranges