@echo off
ngen install BenchmarkingApp.exe /nologo /silent

BenchmarkingApp Workloads/Loading.Competition.workload
BenchmarkingApp Workloads/Sorting.Competition.workload
BenchmarkingApp Workloads/Grouping.Competition.workload
BenchmarkingApp Workloads/Filtering.Competition.workload