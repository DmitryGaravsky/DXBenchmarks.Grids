@echo off
ngen install BenchmarkingApp.exe /nologo /silent

BenchmarkingApp Workloads/Sorting.Deep.workload
BenchmarkingApp Workloads/Filtering.Deep.workload