# Edge Detection Backend in C#

[![Version badge](https://img.shields.io/badge/Version-0.3.0-green.svg)](https://shields.io/)

- [Project Overview](#project-overview)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Running the Application](#running-the-application)
- [Usage](#usage)
- [Example](#example)
- [Test Results](#test-results)
- [References](#references)

## Project Overview
This project implements image edge detection using either the Sobel or Prewitt operator for processing grayscale images. The application is designed to be modular, allowing for easy extension and maintenance.

## Project Structure
```
EdgeDetection
├── src
│   ├── EdgeDetection
│   │   ├── EdgeDetection.csproj
│   │   ├── Program.cs
│   │   ├── Services
│   │   │   ├── EdgeDetectionService.cs
│   │   │   ├── IEdgeDetectionService.cs
│   │   │   └── IEdgeOperator.cs
│   │   └── Operators
│   │       ├── PrewittOperator.cs
│   │       └── SobelOperator.cs
├── UML
│   ├── class_diagram.puml
│   └── class_diagram.png
├── images
│   ├── sample-input.png
│   ├── sample-prewitt.bmp
│   └── sample-sobel.bmp
├── .gitignore
├── README.md
└── EdgeDetection.sln
```

## Getting Started

### Prerequisites
- .NET SDK (version 6.0 or later)

### Installation
1. Clone the repository:
   ```
   git clone https://github.com/NelsenEW/EdgeDetection.git
   ```
2. Navigate to the project directory:
   ```
   cd EdgeDetection
   ```
3. Restore the dependencies:
   ```
   dotnet restore
   ```

### Running the Application
To run the application, execute the following command in the terminal:
```
dotnet run --project src/EdgeDetection/EdgeDetection.csproj <operator> <imagePath>
```
Replace `<operator>` with either `Sobel` or `Prewitt` and `<imagePath>` with the path to the input image.


## Example
Here is an example of an input image and the resulting edge-detected output:

### Input Image
![Input Image](images/sample-input.png)

### Output Image

#### Sobel
![Sobel Image](images/sample-sobel.bmp)

#### Prewitt
![Prewitt Image](images/sample-prewitt.bmp)

## Test Results
The test results and code coverage reports are generated using xunit and cobertura. You can view the detailed test results and coverage analysis in the `test_results` directory.

### View Result
- [Test Result](test_results/test_results.trx)
- [Code Coverage](test_results/index.html)

## References
- [Sobel Operator](https://en.wikipedia.org/wiki/Sobel_operator)
- [Prewitt Operator](https://en.wikipedia.org/wiki/Prewitt_operator)
- [Act Arrange Assert](https://docs.telerik.com/devtools/justmock/basic-usage/arrange-act-assert)