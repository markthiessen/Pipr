namespace Pipr
{
    public interface IStep<Tin, Tout>
    {
        Tout Execute(Tin input, PipelineContext context);
    }
}