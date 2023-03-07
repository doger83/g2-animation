using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2.Animation.Core.Benchmarks.UI;


[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class UIBenchmarks
{
    [Benchmark(Baseline = true)]
    public void UpdateUI_basic()
    {

    }


}
