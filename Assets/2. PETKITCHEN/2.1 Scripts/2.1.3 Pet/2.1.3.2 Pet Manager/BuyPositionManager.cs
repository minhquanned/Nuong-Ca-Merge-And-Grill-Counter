using System.Collections.Generic;
using UnityEngine;

public class BuyPositionManager : MonoBehaviour
{
    [SerializeField] List<RectTransform> listBuySlot;

    public int GetMaxSlot()
    {
        return listBuySlot.Count;
    }

    public Vector2 GetPositionOfSlot(int index)
    {
        return listBuySlot[index].position;
    }
}
