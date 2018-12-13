param ( [switch]$runtime )

####################################################################################
#                   INSTALLER SCRIPT FOR NOREG ON WINDOWS                          #
####################################################################################

# choose your release version
$RELEASE_VERSION="v0.0.2"
### Set install location
$INSTALL_DIR = Join-Path -Path $HOME -ChildPath ".noreg"

echo "Installing NoRegressions...";

# set some variables
$URI="https://github.com/xtellurian/NoRegressions/releases/download/$RELEASE_VERSION/noreg-win10-x64.zip"
$ZIPFILENAME="noreg-$RELEASE_VERSION.zip"
$ZIPFILE = Join-Path -Path $INSTALL_DIR -ChildPath $ZIPFILENAME

if(-Not [System.IO.Directory]::Exists($INSTALL_DIR))
{
    New-Item $INSTALL_DIR -ItemType directory
}
##  download the release  ####
Invoke-WebRequest -Uri $URI -UseBasicParsing -OutFile $ZIPFILE
echo "Downloaded $ZIPFILE"
echo "Installing to $INSTALL_DIR"


Add-Type -AssemblyName System.IO.Compression.FileSystem
[System.IO.Compression.ZipFile]::ExtractToDirectory($ZIPFILE, $INSTALL_DIR)

# create a PS profile if not exists
if(-Not [System.IO.File]::Exists($profile))
{
    echo "Creating PS profile at $profile"
    New-Item -Type file -Force $profile
}

# setup our environment so we can use noreg

$file = Get-Content $profile
$containsWord = $file | %{$_ -match ".noreg"} # this is a weird check.
if ($containsWord -contains $true) {
    Write-Host "Already sourcing this file"
} else {
    echo "Adding to profile at $profile"
    $Env:path += ";$INSTALL_DIR\publish"
    Set-Alias noreg noreg-cli -Scope Global
    # do the same but in the profile
    $SETENV_COMMAND = -join('$Env:path += ', '"',";$INSTALL_DIR\publish", '"')   
    echo $SETENV_COMMAND | Out-File $profile -Append -Encoding UTF8 # add to the path
    echo "Set-Alias noreg noreg-cli -Scope Global"| Out-File $profile -Append -Encoding UTF8 # alias the binary as noreg
    echo "" | Out-File $profile -Append -Encoding UTF8 # newline
}

echo "To uninstall noreg, delete $home\.noreg and remove the relevant lines from $profile"

echo "Run ~ noreg version"