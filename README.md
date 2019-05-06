# Pipr

A simple chain of responsibility pipeline for .NET

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

#### Dependency Injection

You can also have Pipr create your objects for you using the IServiceProvider from `Microsoft.Extensions.DependencyInjection` if you specify the type and the inputs and outputs as you build the pipeline and configure on startup.

In Startup.cs:

```
...

serviceCollection.AddTransient<ToStringStep>(); // (example, doesn't have to be Transient lifetime)
serviceCollection.AddTransient<DoublerStep>();

serviceCollection.AddPipr();
...

```

Then create and use pipelines like so:

```
 var pipeline = new PipelineBuilder()
            .AddStep<ToStringStep, int, string>()
            .AddStep<DoublerStep, int, string, string>()
            .AddStep<DoublerStep, int, string, string>()
            .Build();

var result = pipeline.Execute(12);
```

To satisfy type system constraints, in this setup type, at each step you need to specify inputs and outpt types.
The first step can specify your IStep type and its input and output.
Each additional step must provide the IStep type, the initial step type (pipeline entry type), and the step's input and output types.
