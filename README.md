# Pipr

A simple chain of responsibility pipeline for .NET

### Installing Pipr

You should install [Pipr with NuGet](https://www.nuget.org/packages/Pipr):

    Install-Package Pipr

Or via the .NET Core command line interface:

    dotnet add package Pipr

Either commands, from Package Manager Console or .NET Core CLI, will download
and install Pipr and all required dependencies.

### Usage

#### Simple

First, create some steps that implement IStep<Tin, Tout>.

```
public class ToStringStep : IStep<int, string>
{
    public string Execute(int input, PipelineContext context)
    {
        return input.ToString();
    }
}

public class DoublerStep : IStep<string, string>
{
    public string Execute(string input, PipelineContext context)
    {
        return input + input;
    }
}
```

Then configure a pipeline by adding your steps

```
var pipeline = new PipelineBuilder()
            .AddStep(new ToStringStep())
            .AddStep(new DoublerStep())
            .Build();

var result = pipeline.Execute(12);

// result.Value in this example should be "1212"
```

The in/out of the pipeline can be inferred by the type system as you add steps.

#### Aborting

Pipeline steps can cancel and abort further processing by calling
`context.Cancel()`:

For example

```
public class CancellingStep : IStep<string, string>
{
    public string Execute(string input, PipelineContext context)
    {
        context.Cancel();
        return "a value";
    }
}
```

If a step cancels further processing, no following steps will run, and the
pipeline return value will be the default value for the return type.
