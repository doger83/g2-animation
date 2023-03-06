using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Running;
using g2.Animation.Benchmarks.ParticleSystems;

var summary = BenchmarkRunner.Run<ParticleBenchmarks>();
