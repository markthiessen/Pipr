using System;

namespace Pipr
{
    public static class PipelineBuilderExtensions
    {
        public static StepRunner<T1, TResult> AddStep<T1, T2, TResult>(this StepRunner<T1, T2> pipeline, IStep<T2, TResult> step)
        {
            StepRunner<T2, TResult> next = (StepRunner<T2, TResult>)new StepRunner<T2, TResult>
            {
                Handle = (Func<T2, PipelineContext, TResult>)delegate (T2 arg, PipelineContext context)
                {
                    if (!context.IsCancelled)
                        return step.Execute(arg, context);
                    return default(TResult);
                }
            };
            return new StepRunner<T1, TResult>
            {
                Handle = (Func<T1, PipelineContext, TResult>)delegate (T1 arg, PipelineContext context)
                {
                    if (!context.IsCancelled)
                    {
                        T2 arg2 = pipeline.Execute(arg, context);
                        if (!context.IsCancelled)
                            return ((StepRunner<T2, TResult>)next).Handle(arg2, context);
                        return default(TResult);
                    }
                    return default(TResult);
                }
            };
        }

        public static Pipeline<Tin, Tout> Build<Tin, Tout>(this StepRunner<Tin, Tout> runner)
        {
            return new Pipeline<Tin, Tout>(runner);
        }
    }
}