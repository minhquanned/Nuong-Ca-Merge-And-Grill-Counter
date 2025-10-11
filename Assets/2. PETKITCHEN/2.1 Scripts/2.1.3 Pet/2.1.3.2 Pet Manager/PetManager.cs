using System.Collections.Generic;
using System.Linq;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    [SerializeField] private BuyPositionManager buyPositionManager;

    private Queue<Pet> listPet = new Queue<Pet>();

    public void AddNewPet(Pet pet)
    {
        listPet.Enqueue(pet);
        UpdateOrder();
    }

    [ProButton]
    public void GetCurrent()
    {
        Pet firstPet = listPet.Dequeue();
        firstPet.MoveTo(Vector2.zero);
        foreach (var pet in listPet)
        {
            pet.MoveTo(buyPositionManager.GetPositionOfSlot(pet.GetOrder() - 1));
        }
        UpdateOrder();
    }

    private void UpdateOrder()
    {
        int orderIndex = 0;
        foreach (var pet in listPet)
        {
            pet.SetOrder(orderIndex);
            orderIndex++;
        }
    }
}
