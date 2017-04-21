@echo off
ngen install BenchmarkingApp.exe /nologo /silent

BenchmarkingApp Workloads/Sorting.RadGrid.workload
BenchmarkingApp Workloads/Filtering.RadGrid.workload
BenchmarkingApp Workloads/Searching.RadGrid.workload
BenchmarkingApp Workloads/Loading.RadGrid.workload