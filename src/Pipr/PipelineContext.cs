using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Pipr
{
    public class PipelineContext
    {
        public bool IsCancelled { get; private set; }
        public void Cancel()
        {
            IsCancelled = true;
        }
    }
}