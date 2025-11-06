<#
Demo runner for project 11 - ASP.NET Core async sample
Observe in the output:
- request latency and how long requests take
- how cancellation is handled (look for OperationCanceledException / HTTP 499 mapping)
- server logs showing which side cancelled the operation
#>

Set-StrictMode -Version Latest

dotnet build
dotnet run --project 11-ASPNetCore-Async/11-ASPNetCore-Async.csproj
