using UnityEngine;
using com.cyborgAssets.inspectorButtonPro;
using static Constant;
using Unity.VisualScripting;
using System.Collections.Generic;

public class MatrixGenerator : MonoBehaviour, IGameService
{
    [SerializeField] private SO_Gameplay_RandomRatio soRandomRatio;

    private Table[,] MainTable;
    public void InjectDependencies(ServiceContainer container)
    {

    }

    public void Initialize()
    {

    }

    private void OnEnable()
    {
        GameEventManager.Instance.Subscribe<Match3ItemData>(GameEventKey.OnMatch3Item, OnMatch3Item);
    }

    private void OnDisable()
    {
        GameEventManager.Instance.Unsubscribe<Match3ItemData>(GameEventKey.OnMatch3Item, OnMatch3Item);
    }
    
    private void OnMatch3Item(Match3ItemData data)
    {
        RandomNextValueAt(data.row, data.col);
    }

    private void Start()
    {
        Invoke(nameof(SetUp), 1f);
    }

    private void SetUp()
    {
        MainTable = new Table[k_defaultRow, k_defaultCol];
        
        GridUtils.ForEachCell(k_defaultRow, k_defaultCol, (r, c) =>
        {
            Table table = new Table();
            
            List<int> listRandomOfCurrent = RandomUtility.GetWeightedRandomAllowMaxTwoSame(soRandomRatio.randomWeightEntries);
            List<int> listRandomOfNext = RandomUtility.GetWeightedRandomAllowMaxTwoSame(soRandomRatio.randomWeightEntries);

            for (int i = 0; i < maxItemOfTable; i++)
            {
                Cell cell = new Cell();
                cell.currentValue = listRandomOfCurrent[i];
                cell.nextValue = listRandomOfNext[i];
                table.cells[i] = cell;
            }                
            MainTable[r,c] = table;
        });

        GameEventManager.Instance.Notify(GameEventKey.OnInitMatrixDone, "OnInitMatrixDone");
    }

    private void RandomNextValueAt(int row, int col)
    {
        GridUtils.ForEachCell(k_defaultRow, k_defaultCol, (r, c) =>
        {
            if(r == row && c == col)
            {
                Cell[] cells = MainTable[r, c].cells;
                
                List<int> listRandomOfNext = RandomUtility.GetWeightedRandomAllowMaxTwoSame(soRandomRatio.randomWeightEntries);

                for (int i = 0; i < cells.Length; i++)
                {
                    cells[i].currentValue = cells[i].nextValue;
                    cells[i].nextValue = listRandomOfNext[i];
                }
            }
        });

        GameEventManager.Instance.Notify(GameEventKey.OnMatrixRandomNextItem, new MatrixRandomNextItem() { row = row, col = col });
    }

    /// <summary>
    /// lấy giá trị của các ô trong bảng
    /// </summary>
    public Cell[] GetCells(int row, int col)
    {
        return MainTable[row,col].cells;
    }    

    [ProButton]
    public void ShowLog()
    {
        GridUtils.ForEachCell(k_defaultRow, k_defaultCol, (r, c) =>
        {
            foreach (var i in MainTable[r, c].cells)
            {
                Debug.Log(i.currentValue);
            }

            Debug.Log("-----");
        });
    }    
}

[System.Serializable]
public class Table
{
    public Cell[] cells;

    public Table()
    {
        cells = new Cell[maxItemOfTable];
    }
}

[System.Serializable]
public class Cell
{
    public int currentValue;
    public int nextValue;
}
