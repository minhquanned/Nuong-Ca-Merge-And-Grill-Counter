using System;
using UnityEditor;
using UnityEngine;

public class UITableCell : MonoBehaviour, ICanPutItem
{
    private RectTransform rect;

    private UIInGameItem currentItem;

    public Action OnCellEmpty;
    public Action OnCellFull;

    public RectTransform Rect => rect;

    void Awake()
    {
        rect = transform as RectTransform;
    }

    public int GetItemID()
    {
        if (GetCurrentItem() != null)
        {
            return GetCurrentItem().GetID();
        }
        else
        {
            return -1;
        }
    }

    public void DestroyItem()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        RemoveItem();
    }

    public void RemoveItem()
    {
        currentItem = null;
        OnCellEmpty?.Invoke();
    }

    public bool CanPut(IItem item)
    {
        if (currentItem == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PutItem(UIInGameItem item)
    {
        if (CanPut(item))
        {
            currentItem = item;
            item.SetICanPutItem(this);
            item.ItemIsPutedTo(rect, true);
            OnCellFull?.Invoke();
        }
        else
        {
            Debug.LogWarning($"Không thể đặt Item vào {name} vì đã có item ở đây");
        }

    }

    public IItem GetCurrentItem()
    {
        return currentItem;
    }

    public bool IsEmpty()
    {
        return currentItem == null;
    }

    public RectTransform GetRect()
    {
        return rect;
    }
}
