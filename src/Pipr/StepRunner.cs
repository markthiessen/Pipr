using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Pipr
{
    public class StepRunner<Tin, Tout>
    {
        internal Func<Tin, PipelineContext, Tout> Handle { get; set; }

        public StepRunner()
        {
            Handle = (arg, context) =>
           {
               return default(Tout);
           };
        }

        public Tout Execute(Tin input, PipelineContext context)
        {
            return Handle(input, context);
        }
    }
}