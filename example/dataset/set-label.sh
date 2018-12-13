#!/bin/bash

cd ../../src/cli/
echo
echo "Set label and data type."
echo
echo dotnet run -- update-dataset --id apples -l ApplePinkLady --type SingleClassImage
echo
dotnet run -- update-dataset --id apples -l ApplePinkLady --type SingleClassImage

echo
echo "Set label and data type."
echo
echo dotnet run -- update-dataset --id oranges -l OrangeNavel --type SingleClassImage
echo
dotnet run -- update-dataset --id oranges -l OrangeNavel --type SingleClassImage