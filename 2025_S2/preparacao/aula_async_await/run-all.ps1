<#
Marathon runner: execute projects 11 -> 12 -> 13 sequentially

Objective: Perform a short review pass that:
- starts the ASP.NET Core demo (11) so you can inspect latency/cancellation
- runs the resilience demo (12) to compare internal vs external timeouts
- executes the benchmark suite (13) and review BenchmarkDotNet tables
#>

Set-StrictMode -Version Latest

Write-Host "=== Running project 11 (Web API) ==="
.\run-11.ps1

Write-Host "=== Running project 12 (Resilience Polly demo) ==="
.\run-12.ps1

Write-Host "=== Running project 13 (Benchmarks) ==="
.\run-13.ps1
