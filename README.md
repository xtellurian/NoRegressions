# No Regressions

Deploy machine learning with confidence.

## Introduction 
NoRegressions is a tool for monitoring, evaluating, and testing machine learning models *before* production and *after* experimentation. The operating principle of NoRegressions is that _all_ ML models are black boxes with certain inputs producing expected outputs. With NoRegressions you can apply previously unseen data as inputs to a model, and compare the results with expected outputs. NoRegressions is written in [DotNet Core](https://dotnet.github.io/) and runs on Windows, Mac and Linux.

## Features

NoRegressions provides a CLI that can:

 * Manage [Azure Blobs](https://docs.microsoft.com/en-au/azure/storage/blobs/storage-quickstart-blobs-dotnet) as data sources.
 * Create labelled datasets from csv or directories.
 * Test different model targets with your own datasets.
 * Report results in [ML FLow](https://mlflow.org/).

## Supported Model Targets

 * [Custom Vision Service](https://customvision.ai/)
 * ... more to come


# Install

On Windows:

```powershell
iex ((New-Object System.Net.WebClient).DownloadString('https://raw.githubusercontent.com/xtellurian/NoRegressions/install/install.ps1'))
```

On Linux:

```sh
echo "linux installer not implemented yet!"
```

On Mac:

Homebrew is the easiest way to manage your CLI install. It provides convenient ways to install, update, and uninstall. If you don't have homebrew available on your system, [install homebrew](https://docs.brew.sh/Installation.html) before continuing.

You can install the CLI by updating your brew repository information, and then running the install command:

```sh
brew update && brew tap AussieDevCrew/NoRegressions https://github.com/JasonTheDeveloper/NoRegressions.git && brew install AussieDevCrew/NoRegressions/noreg-cli
```

Now NoRegressions is aliased as `noreg`

# Uninstall

On Mac:

```sh
brew remove AussieDevCrew/NoRegressions/noreg-cli
```

# Sample Commands

There are many more examples in the [examples directory](/example)

 * Help: 
 ```sh
 noreg help
 ```
 * Upload a file to Azure Blob Storage: 

 ```sh
 noreg blob --upload --destination my_container --filepath "/path/to/my_image.jpg"
 ```
* Write URLs from my_container into list.txt
```sh
noreg blob --list-blob my_container --output list.txt
```
 * Create a dataset called my_dataset
```sh
noreg create-dataset --id my_dataset
```
 * Add images to a dataset. Currently we use a `--type` flag to help the test runner understand the test results.
```sh
noreg update-dataset --id my_dataset -l my_label --type SingleClassImage --from-file "list.txt"
```
 * Run a test against a Custom Vision test target with my_dataset.
```sh
noreg test -t CustomVision -s my_dataset
```

## Configuration

You can configure the cli with `noreg config`

# Build and Test

Tools are all DotNet Core, minimum version 2.2, which you can download [here](https://dotnet.microsoft.com/download)

 * Build the CLI: `dotnet build src/cli`
 * Run the tests: `dotnet test test/unit`


# Contribute

This project is very new and there is lots to do :-)

Currently, we are only supporting Custom Vision as a target for testing. We are also only supporting single label images.

## To Do
- [ ] Add multi-class image recognition tests
- [ ] Add object-counting image recognition tests
- [ ] New targets (I don't know which)

