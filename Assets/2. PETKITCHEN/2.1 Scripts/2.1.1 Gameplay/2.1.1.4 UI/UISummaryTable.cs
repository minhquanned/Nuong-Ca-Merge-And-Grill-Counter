using UnityEngine;
using static Constant;

public class UISummaryTable : MonoBehaviour, IGameService
{
    public UITable[] listTable {get; private set;}

    private DropItemController dropItemController;

    public void InjectDependencies(ServiceContainer container)
    {

    }

    public void Initialize()
    {

    }

    void Awake()
    {
        listTable = GetComponentsInChildren<UITable>();
        SetRowCol();
    }

    private void SetRowCol()
    {
        for(int i = 0; i < listTable.Length; i++)
        {
            listTable[i].SetRowCol(i / k_defaultCol, i % k_defaultCol);
        }
    }

    private void Start()
    {

    }

    public UITable GetUITableAt(int row, int col)
    {
        for (int i = 0; i < listTable.Length; i++)
        {
            if (listTable[i].row == row && listTable[i].col == col)
            {
                return listTable[i];
            }    
        }

        return null;
    }    
}
