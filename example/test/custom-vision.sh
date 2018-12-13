#!/bin/bash

cd ../../src/cli/
echo
echo "Test apples dataset using Custom Vision."
echo
echo dotnet run -- test -t CustomVision -s apples
echo
dotnet run -- test -t CustomVision -s apples

echo
echo "Test oranges dataset using Custom Vision."
echo
echo dotnet run -- test -t CustomVision -s oranges
echo
dotnet run -- test -t CustomVision -s oranges