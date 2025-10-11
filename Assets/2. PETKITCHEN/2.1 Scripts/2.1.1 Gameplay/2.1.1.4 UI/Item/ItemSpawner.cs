using System;
using UnityEngine;
using static Constant;
using static UnityEngine.Rendering.DebugUI.Table;

public class ItemSpawner : MonoBehaviour, IGameService
{
    private MatrixGenerator matrixGenerator;
    private ItemDatabase itemDatabase;
    private UISummaryTable uISummaryTable;
    private DropItemController dropItemController;

    [SerializeField] private GameObject UIInGameItemPrefab;

    public void Initialize()
    {

    }

    public void InjectDependencies(ServiceContainer container)
    {
        container.TryToGetService(out matrixGenerator);
        container.TryToGetService(out uISummaryTable);
        container.TryToGetService(out itemDatabase);
        container.TryToGetService(out dropItemController);
    }

    private void OnEnable()
    {
        GameEventManager.Instance.Subscribe<string>(GameEventKey.OnInitMatrixDone, OnMatrixInitDone);
        GameEventManager.Instance.Subscribe<MatrixRandomNextItem>(GameEventKey.OnMatrixRandomNextItem, OnRandomNextItem);
    }

    private void OnDisable()
    {
        GameEventManager.Instance.Unsubscribe<string>(GameEventKey.OnInitMatrixDone, OnMatrixInitDone);
        GameEventManager.Instance.Unsubscribe<MatrixRandomNextItem>(GameEventKey.OnMatrixRandomNextItem, OnRandomNextItem);
    }

    private void OnMatrixInitDone(string text)
    {
        CreateAllItems();
    }    

    public void CreateAllItems()
    {
        GridUtils.ForEachCell(k_defaultRow, k_defaultCol, callbackRepeat: (row, col) =>
        {
            Cell[] cells = matrixGenerator.GetCells(row, col);
            for(int indexCell = 0; indexCell < cells.Length; indexCell++)
            {
                UITable tb = uISummaryTable.GetUITableAt(row, col);
                UITableCell cell = tb.ListUITableCell[indexCell];
                UITableCell cellView = tb.ListUITableCellView[indexCell];

                CreateNewItem(cells[indexCell].currentValue , cell);
                UpdateNewItemInNextTable(cells[indexCell].nextValue, cellView);
            }
        });
    }

    private void OnRandomNextItem(MatrixRandomNextItem matrixRandomNextItem)
    {
        CreateNewItemAt(matrixRandomNextItem.row, matrixRandomNextItem.col);
    }    

    private void CreateNewItemAt(int row, int col)
    {
        Cell[] cells = matrixGenerator.GetCells(row, col);
        for (int indexCell = 0; indexCell < cells.Length; indexCell++)
        {
            UITable tb = uISummaryTable.GetUITableAt(row, col);
            UITableCell cell = tb.ListUITableCell[indexCell];
            UITableCell cellView = tb.ListUITableCellView[indexCell];

            CreateNewItem(cells[indexCell].currentValue, cell);
            UpdateNewItemInNextTable(cells[indexCell].nextValue, cellView);
        }
    }

    private void CreateNewItem(int ID, UITableCell cell)
    {
        if (ID == -1) return;
        if(itemDatabase.TryToGetItemByID(ID, out SO_ItemBase item))
        {
            var uiItemInGame = Instantiate(UIInGameItemPrefab).GetComponent<UIInGameItem>();
            uiItemInGame.AllowCanDragable(true);
            uiItemInGame.AssignManager(dropItemController);
            uiItemInGame.UpdateData(item);
            cell.PutItem(uiItemInGame);
        }
    }

    private void UpdateNewItemInNextTable(int ID, UITableCell cell)
    {
        if (ID == -1) return;
        if (itemDatabase.TryToGetItemByID(ID, out SO_ItemBase item))
        {
            var uiItemInGame = Instantiate(UIInGameItemPrefab).GetComponent<UIInGameItem>();
            uiItemInGame.AllowCanDragable(false);
            uiItemInGame.AssignManager(dropItemController);
            uiItemInGame.UpdateData(item);
            cell.PutItem(uiItemInGame);
        }
    }    
}
