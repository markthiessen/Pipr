using System;

namespace Pipr
{
    public class StepRunner<Tin, Tout>
    {
        internal Func<Tin, PipelineContext, Tout> Handle { get; set; }

        public StepRunner()
        {
            Handle = ((Tin arg, PipelineContext context) => default(Tout));
        }

        public Tout Execute(Tin input, PipelineContext context)
        {
            return Handle(input, context);
        }
    }
}