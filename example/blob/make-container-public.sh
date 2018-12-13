#!/bin/bash

cd ../../src/cli/
echo
echo "Set container 'apples' public."
echo
echo dotnet run -- blob --make-container-public apples
echo
dotnet run -- blob --make-container-public apples

echo
echo "Set container 'oranges' public."
echo
echo dotnet run -- blob --make-container-public oranges
echo
dotnet run -- blob --make-container-public oranges