#!/bin/bash
set -e
NOREG_VERSION="v0.0.2"

echo "Installing NoRegressions..."
URL="https://github.com/xtellurian/NoRegressions/releases/download/$NOREG_VERSION/noreg-linux-x64.zip"

# download the file
mkdir -p  ~/.noreg
wget $URL -O ~/.noreg/noreg-$NOREG_VERSION.zip

#unzip the file into ~/.noreg
unzip ~/.noreg/noreg-$NOREG_VERSION.zip -d ~/.noreg

chmod +x ~/.noreg/publish/noreg-cli
alias noreg='~/.noreg/publish/noreg-cli'
echo alias noreg='~/.noreg/publish/noreg-cli' >> ~/.bashrc

# remove the zip
rm ~/.noreg/noreg-$NOREG_VERSION.zip

echo "You may need to run: apt-get install icu-devtools"
echo "Remember to source ~/.bashrc"
