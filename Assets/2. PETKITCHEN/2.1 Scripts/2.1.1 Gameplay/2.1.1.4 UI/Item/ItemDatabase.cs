using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour, IGameService
{
    [SerializeField] private List<SO_ItemBase> listItemConfigs;

    private ItemSpawner _itemSpawner;

    public void InjectDependencies(ServiceContainer container)
    {
        container.TryToGetService(out _itemSpawner);
    }

    public void Initialize()
    {

    }

    private void CreateAllItems()
    {

    }    

    public bool TryToGetItemByID(int id, out SO_ItemBase result)
    {
        result =  listItemConfigs.Find((i) => i.ID == id);
        if (result != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
