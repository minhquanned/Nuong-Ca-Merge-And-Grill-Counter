using System.Collections.Generic;
using UnityEngine;

public class PetSpawner : MonoBehaviour
{
    [SerializeField] private BuyPositionManager BuyPositionManager;
    [SerializeField] private PetManager petManager;

    [SerializeField] private List<Pet> PetPrefabs;

    private void Start()
    {
        SpawnAllPetAfterStartGame();
    }

    private void SpawnAllPetAfterStartGame()
    {
        for (int i = 0; i < BuyPositionManager.GetMaxSlot(); i++)
        {
            SpawnPet(BuyPositionManager.GetPositionOfSlot(i));
        }
    }

    private Pet SpawnPet(Vector3 position)
    {
        Pet newPet = Instantiate(GetRandomPetPrefab(), position, Quaternion.identity, transform);
        newPet.transform.SetAsFirstSibling();
        newPet.name += Random.Range(0, 999);
        petManager.AddNewPet(newPet);
        return newPet;
    }

    private Pet GetRandomPetPrefab()
    {
        return PetPrefabs[Random.Range(0, PetPrefabs.Count)];   
    }
}
