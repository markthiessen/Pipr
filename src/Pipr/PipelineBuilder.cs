using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Pipr
{
    public class PipelineBuilder
    {
        public StepRunner<Tin, Tout> AddStep<T, Tin, Tout>() where T : IStep<Tin, Tout>
        {
            IStep<Tin, Tout> step = PipelineConfiguration.ServiceProvider.GetService<T>();
            return new StepRunner<Tin, Tout>
            {
                Handle = (arg, context) => step.Execute(arg, context)
            };
        }

        public StepRunner<Tin, Tout> AddStep<Tin, Tout>(IStep<Tin, Tout> step)
        {
            return new StepRunner<Tin, Tout>
            {
                Handle = (arg, context) => step.Execute(arg, context)
            };
        }
    }

    public static class PipelineBuilderExtensions
    {
        public static StepRunner<T1, TResult> AddStep<T, T1, T2, TResult>(this StepRunner<T1, T2> pipeline) where T : IStep<T2, TResult>
        {
            IStep<T2, TResult> step = PipelineConfiguration.ServiceProvider.GetService<T>();
            var next = new StepRunner<T2, TResult>
            {
                Handle = (arg, context) => step.Execute(arg, context)
            };

            return new StepRunner<T1, TResult>
            {
                Handle = (arg, context) =>
                {
                    if (context.IsCancelled)
                    {
                        return default(TResult);
                    }
                    else
                    {
                        var pipelineResultSoFar = pipeline.Execute(arg, context);
                        if (context.IsCancelled)
                        {
                            return default(TResult);
                        }
                        return next.Handle(pipelineResultSoFar, context);
                    }
                }
            };
        }

        public static StepRunner<T1, TResult> AddStep<T1, T2, TResult>(this StepRunner<T1, T2> pipeline, IStep<T2, TResult> step)
        {
            var next = new StepRunner<T2, TResult>
            {
                Handle = (arg, context) =>
                {
                    if (context.IsCancelled)
                    {
                        return default(TResult);
                    }
                    return step.Execute(arg, context);
                }
            };

            return new StepRunner<T1, TResult>
            {
                Handle = (arg, context) =>
                {
                    if (context.IsCancelled)
                    {
                        return default(TResult);
                    }
                    else
                    {
                        var pipelineResultSoFar = pipeline.Execute(arg, context);
                        if (context.IsCancelled)
                        {
                            return default(TResult);
                        }
                        return next.Handle(pipelineResultSoFar, context);
                    }
                }
            };
        }

        public static Pipeline<Tin, Tout> Build<Tin, Tout>(this StepRunner<Tin, Tout> runner)
        {
            return new Pipeline<Tin, Tout>(runner);
        }
    }
}