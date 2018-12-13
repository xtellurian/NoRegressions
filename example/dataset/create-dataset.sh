#!/bin/bash

cd ../../src/cli/
echo
echo "Create new dataset for data with id 'apples'"
echo
echo dotnet run -- create-dataset --id apples
echo
dotnet run -- create-dataset --id apples

echo
echo "Create new dataset for data with id 'oranges'"
echo
echo dotnet run -- create-dataset --id oranges
echo
dotnet run -- create-dataset --id oranges