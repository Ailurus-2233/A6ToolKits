using Avalonia;

namespace A6ToolKits.Bootstrapper.Interfaces;

public interface IBootstrapper
{
    void Initialize();
    void Configure();
    void OnCompleted();
    void Run(string[] args);
}