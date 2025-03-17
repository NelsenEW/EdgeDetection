# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

# Install dependencies for System.Drawing.Common and libgdiplus
RUN apt-get update && apt-get install -y --no-install-recommends \
        libc6-dev \
        libgdiplus \
        libx11-dev \
        libtiff5 \
        libpng-dev \
        libjpeg-dev \
        libgif-dev \
        libexif-dev \
        glibc-source \
        libxml2 \
        libunwind8 \
    && rm -rf /var/lib/apt/lists/*

# Copy solution and project files separately to leverage Docker caching
COPY *.sln .
COPY src/EdgeDetection/EdgeDetection.csproj src/EdgeDetection/
COPY tests/EdgeDetection.Tests/EdgeDetection.Tests.csproj tests/EdgeDetection.Tests/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY src/ src/
COPY tests/ tests/

# Build the application
RUN dotnet build src/EdgeDetection/EdgeDetection.csproj -c Release --no-restore -o /app/build

# Publish the application
RUN dotnet publish src/EdgeDetection/EdgeDetection.csproj -c Release --no-restore -o /app/publish

# Test Stage
FROM build AS test

WORKDIR /app

# Build test project
RUN dotnet build tests/EdgeDetection.Tests/EdgeDetection.Tests.csproj -c Release -o /app/testbuild


RUN mkdir -p /app/test_results /app/coverage

# Install coverlet
RUN dotnet tool install --global coverlet.console

RUN dotnet tool install --global dotnet-reportgenerator-globaltool --version 5.0.0

RUN dotnet tool install --global trxlog2html --version 1.0.2

ENV PATH="$PATH:/root/.dotnet/tools"

# Generate code coverage report in HTML format
CMD dotnet test tests/EdgeDetection.Tests/EdgeDetection.Tests.csproj \
    /p:CoverletOutputFormat=cobertura \
    --results-directory /app/test_results \
    --logger "trx;LogFileName=test_results.trx" \
    --collect "XPlat Code Coverage" && \
    reportgenerator -reports:/app/test_results/**/coverage.cobertura.xml \
    -targetdir:/app/test_results/coverage_report \
    -reporttypes:Html && \
    find /app/test_results -mindepth 1 -maxdepth 1 ! -name 'coverage_report' ! -name 'test_results.trx' -exec rm -rf {} + && \
    mv /app/test_results/coverage_report/* /app/test_results/ && \
    rm -rf /app/test_results/coverage_report && \
    trxlog2html -i /app/test_results/test_results.trx -o /app/test_results/test_results.html

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app

# Install required dependencies
RUN apt-get update && apt-get install -y --no-install-recommends \
        libc6-dev \
        libgdiplus \
        libx11-dev \
        libtiff5 \
        libpng-dev \
        libjpeg-dev \
        libgif-dev \
        libexif-dev \
    && rm -rf /var/lib/apt/lists/*

# Set environment variables for System.Drawing on Linux
ENV LD_LIBRARY_PATH=/usr/lib
ENV FONTCONFIG_PATH=/etc/fonts
ENV System.Drawing.EnableUnixSupport=1
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1

# Copy published application from build stage
COPY --from=build /app/publish .

# Copy coverage report from test stage
COPY --from=test /app/test_results/coverage_report /app/coverage_report

# Set entrypoint
ENTRYPOINT ["dotnet", "EdgeDetection.dll"]