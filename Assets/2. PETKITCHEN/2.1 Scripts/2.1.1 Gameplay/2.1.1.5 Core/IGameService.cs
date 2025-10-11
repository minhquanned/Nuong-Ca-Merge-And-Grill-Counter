//using System.ComponentModel.Design;

public interface IGameService
{
    void InjectDependencies(ServiceContainer container);
    void Initialize();
}