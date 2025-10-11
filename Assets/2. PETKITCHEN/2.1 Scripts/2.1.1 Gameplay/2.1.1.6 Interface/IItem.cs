
using UnityEngine;

public interface IItem
{
    public void SetICanPutItem(ICanPutItem canPutItem);
    public void ItemIsPutedTo(RectTransform rect, bool isStrectFull);

    public ICanPutItem GetCurrentICanPutItem();

    int GetID();
}