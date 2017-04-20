@echo off
ngen install BenchmarkingApp.exe /nologo /silent

BenchmarkingApp Workloads/Sorting.Huge.workload
BenchmarkingApp Workloads/Filtering.Huge.workload
BenchmarkingApp Workloads/Loading.Huge.workload
BenchmarkingApp Workloads/Searching.Huge.workload

BenchmarkingApp Workloads/Sorting.Deep.workload
BenchmarkingApp Workloads/Filtering.Deep.workload