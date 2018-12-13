#!/bin/bash

cd ../../src/cli/
echo
echo "Add image urls of apples from blob storage to dataset."
echo
echo dotnet run -- update-dataset --id apples -l ApplePinkLady --type SingleClassImage --from-file ../../example/apples.txt --replace-all
echo
dotnet run -- update-dataset --id apples -l ApplePinkLady --type SingleClassImage --from-file ../../example/apples.txt --replace-all

echo
echo "Add image urls of oranges from blob storage to dataset."
echo
echo dotnet run -- update-dataset --id oranges -l OrangeNavel --type SingleClassImage --from-file ../../example/oranges.txt --replace-all
echo
dotnet run -- update-dataset --id oranges -l OrangeNavel --type SingleClassImage --from-file ../../example/oranges.txt --replace-all