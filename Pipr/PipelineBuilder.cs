using System;

namespace Pipr
{
    public class PipelineBuilder
    {
        public StepRunner<Tin, Tout> AddStep<Tin, Tout>(IStep<Tin, Tout> step)
        {
            return new StepRunner<Tin, Tout>
            {
                Handle = (Func<Tin, PipelineContext, Tout>)((Tin arg, PipelineContext context) => step.Execute(arg, context))
            };
        }
    }
}