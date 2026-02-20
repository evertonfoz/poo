<#
Demo runner for project 13 - Benchmarks (BenchmarkDotNet)
Observe in the output:
- the BenchmarkDotNet tables: Mean, StdDev, Error and Confidence Intervals
- GC/Allocation stats and how they affect interpretation
- p95/p99 and whether improvements are meaningful vs noise
#>

Set-StrictMode -Version Latest

dotnet build
dotnet run -c Release --project 13-Benchmarks/13-Benchmarks.csproj
