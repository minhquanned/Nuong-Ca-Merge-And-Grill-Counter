using System.Linq;
using System.Threading;
using UnityEngine;

public class UITable : MonoBehaviour
{

    private RectTransform rect;
    [SerializeField] private UITableCell[] listUITableCell;
    [SerializeField] private UITableCell[] listUITableCellView;

    public RectTransform Rect => rect;

    public int row { get; private set; }
    public int col { get; private set; }

    public UITableCell[] ListUITableCell => listUITableCell;
    public UITableCell[] ListUITableCellView => listUITableCellView;

    public void SetRowCol(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    private void Awake()
    {
        rect = transform as RectTransform;
    }

    private void OnEnable()
    {
        foreach (var cell in listUITableCell)
        {
            cell.OnCellFull += WhenAnyCellIsFill;
            cell.OnCellEmpty += WhenAnyCellIsEmpty;
        }
    }

    private void OnDisable()
    {
        foreach (var cell in listUITableCell)
        {
            cell.OnCellFull -= WhenAnyCellIsFill;
            cell.OnCellEmpty -= WhenAnyCellIsEmpty;
        }
    }

    private void WhenAnyCellIsFill()
    {
        bool isMatch = IsAllCellMatch();
        if (isMatch)
        {
            foreach (var cell in listUITableCell)
            {
                if (cell.transform.childCount > 0)
                {
                    cell.DestroyItem();
                }
            }

            foreach (var cell in listUITableCellView)
            {
                if (cell.transform.childCount > 0)
                {
                    cell.DestroyItem();
                }
            }

            Match3ItemData newData = new Match3ItemData()
            {
                row = row,
                col = col,
                id = listUITableCell[0].GetItemID()
            };
            GameEventManager.Instance.Notify(GameEventKey.OnMatch3Item, newData);
        }
    }

    private void WhenAnyCellIsEmpty()
    {
        if (IsAllCellEmpty())
        {
            Match3ItemData newData = new Match3ItemData()
            {
                row = row,
                col = col,
                id = listUITableCell[0].GetItemID()
            };
            GameEventManager.Instance.Notify(GameEventKey.OnMatch3Item, newData);
        }
    }

    private bool IsAllCellMatch()
    {
        int count = 0;
        for (int i = 1; i < listUITableCell.Length; i++)
        {
            if (listUITableCell[0].GetItemID() == listUITableCell[i].GetItemID())
            {
                count++;
            }
        }

        if (count == listUITableCell.Length - 1)
        {
            return true;
        }

        return false;
    }

    private bool IsAllCellEmpty()
    {
        int count = 0;
        for (int i = 0; i < listUITableCell.Length; i++)
        {
            Debug.Log(listUITableCell[i].IsEmpty());
            if (listUITableCell[i].IsEmpty())
            {
                count++;
            }
        }

        if (count == listUITableCell.Length)
        {
            Debug.Log("Tất cả cell đều rỗng");
            return true;
        }

        return false;
    }
}
