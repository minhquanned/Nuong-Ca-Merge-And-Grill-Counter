using System;
using System.Collections.Generic;

public class ServiceContainer
{
    private List<IGameService> _services = new List<IGameService>();

    public void AddService(IGameService service)
    {
        if (!_services.Contains(service))
        {
            _services.Add(service);
        }
    }

    public bool TryToGetService<T>(out T outService) where T : class, IGameService
    {
        foreach (var service in _services)
        {
            if (service is T matched)
            {
                outService = matched;
                return true;
            }
        }

        outService = null;
        return false;
    }
}
