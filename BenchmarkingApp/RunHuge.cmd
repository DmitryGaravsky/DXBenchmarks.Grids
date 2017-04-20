@echo off
ngen install BenchmarkingApp.exe /nologo /silent

BenchmarkingApp Workloads/Sorting.Huge.workload
BenchmarkingApp Workloads/Filtering.Huge.workload
BenchmarkingApp Workloads/Searching.Huge.workload
BenchmarkingApp Workloads/Loading.Huge.workload