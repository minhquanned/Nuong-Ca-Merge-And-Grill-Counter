using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class DropItemController : MonoBehaviour, IGameService, ICanPutItem
{
    private UISummaryTable summaryTable;

    private RectTransform rect;

    private UIInGameItem currentItemDragging;

    public void InjectDependencies(ServiceContainer container)
    {
        container.TryToGetService(out summaryTable);
    }

    public void Initialize()
    {
        rect = transform as RectTransform;
    }

    public void StartDragItem(UIInGameItem uIInGameItem)
    {
        PutItem(uIInGameItem);
    }

    public void DropItem(UIInGameItem uIInGameItem)
    {
        Vector2 pos = uIInGameItem._rectTransform.position;

        if (TryToFindTableContainPoint(pos, out UITable table) &&
            TryToFindCellContainPoint(pos, table, out UITableCell cell) &&
            cell.CanPut(uIInGameItem))
        {
            uIInGameItem.GetCurrentICanPutItem().RemoveItem();
            cell.PutItem(uIInGameItem);
        }
        else
        {
            PutBackToOriginal(uIInGameItem);
            Debug.LogError("[Drop Item]: Ngoài tầm hoặc không thể đặt");
        }
    }

    private void PutBackToOriginal(UIInGameItem item)
    {
        item.TeleportToCurrentSeat();
    }    

    private bool TryToFindTableContainPoint(Vector3 screenPoint, out UITable uITable)
    {
        for(int i = 0; i < summaryTable.listTable.Length; i ++)
        {
            if(RectTransformUtility.RectangleContainsScreenPoint(summaryTable.listTable[i].Rect, screenPoint))
            {
                uITable = summaryTable.listTable[i];
                return true;
            }
        }

        uITable = null;
        return false;
    }

    private bool TryToFindCellContainPoint(Vector3 screenPoint, UITable uITable,out UITableCell uITableCell)
    {
        for(int i = 0; i < uITable.ListUITableCell.Length; i ++)
        {
            if(RectTransformUtility.RectangleContainsScreenPoint(uITable.ListUITableCell[i].Rect, screenPoint))
            {
                uITableCell = uITable.ListUITableCell[i];
                return true;
            }
        }

        uITableCell = null;
        return false;
    }

    public bool CanPut(IItem item)
    {
        return true;
    }

    public void PutItem(UIInGameItem item)
    {
        currentItemDragging = item;
        item.ItemIsPutedTo(rect, false);
    }

    public IItem GetCurrentItem()
    {
        return currentItemDragging;
    }

    public void RemoveItem()
    {
        currentItemDragging = null;
    }

    public RectTransform GetRect()
    {
        return rect;
    }
}
