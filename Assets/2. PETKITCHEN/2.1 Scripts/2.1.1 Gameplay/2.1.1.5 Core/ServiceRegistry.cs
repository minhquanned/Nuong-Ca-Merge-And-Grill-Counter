using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ServiceRegistry : MonoBehaviour
{
    private IGameService[] allServices;

    private ServiceContainer container;

    private void Awake()
    {
        allServices = GetComponentsInChildren<IGameService>(true);
        container = new ServiceContainer();
        AddAllService();
        InjectAllServices(container);
    }

    private void AddAllService()
    {
        foreach (var service in allServices)
        {
            container.AddService(service);
        }
    }    

    private void InjectAllServices(ServiceContainer container)
    {
        foreach (var service in allServices)
        {
            service.InjectDependencies(container);
            service.Initialize();
        }
    }
}