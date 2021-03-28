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