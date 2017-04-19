@echo off
ngen install BenchmarkingApp.exe /nologo /silent

BenchmarkingApp Workloads/Sorting.workload
BenchmarkingApp Workloads/Searching.workload
BenchmarkingApp Workloads/Loading.workload
BenchmarkingApp Workloads/Filtering.workload
