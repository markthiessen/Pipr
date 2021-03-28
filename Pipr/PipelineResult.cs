namespace Pipr
{
    public class PipelineResult<T>
    {
        public PipelineContext Context { get; internal set; }
        public bool Completed { get; internal set; }
        public T Value { get; internal set; }
    }
}