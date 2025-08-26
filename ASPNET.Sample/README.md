# Project Setup Guide

To run this project, you need to ensure you have the correct ASP.NET Core SDK installed and that your `project.json` file references the appropriate version.

## Prerequisites

Before building or running this project, you must install the ASP.NET Core SDK. This project was developed with and requires **ASP.NET Core SDK 8.0.16**.

Download the SDK from the official Microsoft documentation:
[https://dotnet.microsoft.com/download/dotnet/8.0](https://dotnet.microsoft.com/download/dotnet/8.0)

## Project Configuration

Ensure your `project.json` file contains the correct reference for the ASP.NET Core App Runtime. Locate the `dependencies` section and verify the `Microsoft.AspNetCore.App.Runtime.win-x64` entry matches the required version:

```json
{
  "dependencies": {
    "Microsoft.AspNetCore.App.Runtime.win-x64": "[8.0.16]",
    // Other dependencies...
  },
  // Other project settings...
}
```

If the version does not match, please update it to `[8.0.16]` to ensure compatibility and prevent build issues.

## Running this in production

If you need to run this project on a production robot, make sure to install the ASP .NET Core Runtime of that specific version.