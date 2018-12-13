#!/bin/bash

cd ../../src/cli/
echo
echo "List all containers in Azure Blob Storage."
echo
echo dotnet run -- blob --list-container
echo
dotnet run -- blob --list-container