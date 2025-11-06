#!/usr/bin/env bash
# Marathon: run projects 11 -> 12 -> 13 in order
# Objective: perform a compact review session running the webapi (11),
# then resilience demo (12) and finally the benchmarks (13). Observe how
# cancellation and timeouts show up in 11/12 and how BenchmarkDotNet reports
# results in 13. Use this to compare behavior across samples.

set -euo pipefail

echo "=== Running project 11 (Web API) ==="
./run-11.sh

echo "=== Running project 12 (Resilience Polly demo) ==="
./run-12.sh

echo "=== Running project 13 (Benchmarks) ==="
./run-13.sh
