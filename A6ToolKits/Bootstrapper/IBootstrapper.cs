namespace A6ToolKits.Bootstrapper;

public interface IBootstrapper
{
    void Initialize();
    void Configure();
    void OnCompleted();
    void Run(string[] args);
}