#!/bin/bash

cd ../../src/cli/
echo
echo "List all datasets"
echo
echo dotnet run -- list-dataset
echo
dotnet run -- list-dataset