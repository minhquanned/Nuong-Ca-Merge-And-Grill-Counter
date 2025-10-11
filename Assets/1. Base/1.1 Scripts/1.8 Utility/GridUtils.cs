using System;

public static class GridUtils
{
    public static void ForEachCell(int rowCount, int colCount, Action<int, int> callbackRepeat)
    {
        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                callbackRepeat.Invoke(row, col);
            }
        }
    }
}