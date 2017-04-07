using System;
using System.Windows.Forms;

namespace BenchmarkingApp
{
    public interface IHostService {
        void Show(IBenchmarkHost host);
    }
}
