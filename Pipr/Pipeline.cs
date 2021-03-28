namespace Pipr
{
    public class Pipeline<Tin, Tout>
    {
        public PipelineContext Context { get; internal set; } = new PipelineContext();
        private StepRunner<Tin, Tout> _runner;

        public Pipeline(StepRunner<Tin, Tout> runner)
        {
            _runner = runner;
        }

        public PipelineResult<Tout> Execute(Tin input)
        {
            var result = _runner.Execute(input, Context);

            return new PipelineResult<Tout>
            {
                Context = Context,
                Completed = !Context.IsCancelled,
                Value = result
            };
        }
    }
}