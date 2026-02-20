#!/usr/bin/env bash
# Demo runner for project 12 - Resilience with Polly
# Observe in the output:
# - differences between internal (CancellationTokenSource) vs external (Polly) timeouts
# - retry attempts, backoff timings and which exceptions trigger retries
# - how idempotency concerns appear when retries are applied

set -euo pipefail

dotnet build
dotnet run --project 12-Resilience-Polly/12-Resilience-Polly.csproj
