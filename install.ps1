param ( [switch]$install )

####################################################################################
#                   INSTALLER SCRIPT FOR NOREG ON WINDOWS                          #
####################################################################################

# choose your release version
$RELEASE_VERSION="v0.0.1"
### Set install location
$INSTALL_DIR = Join-Path -Path $HOME -ChildPath ".noreg"

function Install-NoRegressions{
    echo "Installing NoRegressions...";

    # check prerequestit dotnet version
    $DOTNET_VERSION = dotnet --version

    if ( $DOTNET_VERSION -lt 2.2 )
    {
        echo "dotnet version must be 2.2 or greater"
        exit 1
    } else {
        echo "Dotnet version $DOTNET_VERSION is OK"
    }

    # set some variables
    
    $URI="https://github.com/xtellurian/NoRegressions/releases/download/$RELEASE_VERSION/noreg.zip"
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

    # download this file into the right location
    $FILE_TO_SOURCE =  Join-Path -Path $INSTALL_DIR -ChildPath "install.ps1"
    Invoke-WebRequest -Uri "https://raw.githubusercontent.com/xtellurian/NoRegressions/install/install.ps1" -UseBasicParsing -OutFile $FILE_TO_SOURCE
    # source this file when booting powershell
    $startup_command = ". $INSTALL_DIR\install.ps1"
    $file = Get-Content $profile
    $containsWord = $file | %{$_ -match ".noreg"}
    if ($containsWord -contains $true) {
        Write-Host "Already sourcing this file"
    } else {
        echo "adding to profile"
        echo $startup_command | Out-File $profile -Append # adds newline to profile
        echo "" | Out-File $profile -Append # newline
    }
   
}

$global:NOREG_DLL_PATH = Join-Path -Path $INSTALL_DIR -ChildPath "netcoreapp2.2\cli.dll"
echo "noreg path is $global:NOREG_DLL_PATH"
if($install)
{
    Install-NoRegressions
}

function global:Execute-NoRegressions {
    dotnet $global:NOREG_DLL_PATH $args
}

Set-Alias noreg Execute-NoRegressions -Scope Global
