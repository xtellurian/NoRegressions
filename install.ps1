param ( [switch]$install )

####################################################################################
#                   INSTALLER SCRIPT FOR NOREG ON WINDOWS                          #
####################################################################################

# choose your release version
$RELEASE_VERSION="v0.0.1"

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

    ##  download the release  ####
    $URI="https://github.com/xtellurian/NoRegressions/releases/download/$RELEASE_VERSION/noreg.zip"
    $OUTFILENAME="noreg-$RELEASE_VERSION.zip"
    $OUTFILE = Join-Path -Path $PSScriptRoot -ChildPath $OUTFILENAME

    Invoke-WebRequest -Uri $URI -UseBasicParsing -OutFile $OUTFILE
    echo "Downloaded $OUTFILE"
    echo "Extracting to $BINARY_DIR"

    Add-Type -AssemblyName System.IO.Compression.FileSystem
    [System.IO.Compression.ZipFile]::ExtractToDirectory($OUTFILE, $BINARY_DIR)

    # create a PS profile if not exists
    if(-Not [System.IO.File]::Exists($profile))
    {
        echo "Creating PS profile at $profile"
        New-Item -Type file -Force $profile
    }

    Set-Variable -Name NOREG_DLL_PATH -Value $NOREG_DLL_PATH -Scope Global -Force

    # source this file when booting powershell
    $startup_command = ". $PSScriptRoot\install.ps1"
    $file = Get-Content $profile
    $containsWord = $file | %{$_ -match "NoRegressions"}
    if ($containsWord -contains $true) {
        Write-Host "Already sourcing this file"
    } else {
        echo "adding to profile"
        echo $startup_command | Out-File $profile -Append # adds newline to profile
        echo "" | Out-File $profile -Append # newline
    }
   
}

### Set internal variables

$BINARY_DIR = Join-Path -Path $PSScriptRoot -ChildPath "bin"
$global:NOREG_DLL_PATH = Join-Path -Path $BINARY_DIR -ChildPath "netcoreapp2.2\cli.dll"

if($install)
{
    Install-NoRegressions
}

function global:Execute-NoRegressions {
    dotnet $global:NOREG_DLL_PATH $args
}

Set-Alias noreg Execute-NoRegressions -Scope Global
