using UnityEngine;

public class Pet : MonoBehaviour
{
    private PetMoverment petMoverment;

    private int orderIndex;

    void Awake()
    {
        petMoverment = GetComponent<PetMoverment>();
    }

    public int GetOrder()
    {
        return orderIndex; 
    }

    public void SetOrder(int index) // số thứ tự
    {
        orderIndex = index;
    }

    public void MoveTo(Vector3 target)
    {
        petMoverment.MoveTo(target);
    }
}