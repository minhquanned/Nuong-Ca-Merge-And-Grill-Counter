using System.Collections.Generic;
using UnityEngine;

public static class RandomUtility
{
    public static int GetWeightedRandom(List<RandomWeightEntry> entries)
    {
        float totalWeight = 0f;
        foreach (var entry in entries)
            totalWeight += entry.weight;

        float rand = Random.Range(0f, totalWeight);

        float cumulative = 0f;
        foreach (var entry in entries)
        {
            cumulative += entry.weight;
            if (rand <= cumulative)
            {
                // Chọn ngẫu nhiên 1 giá trị trong nhóm values
                int index = Random.Range(0, entry.values.Count);
                return entry.values[index];
            }
        }

        // Dự phòng (không nên xảy ra nếu weight hợp lệ)
        return entries[entries.Count - 1].values[0];
    }

    public static List<int> GetWeightedRandomAllowMaxTwoSame(List<RandomWeightEntry> entries)
    {
        List<int> result = new List<int>();
        int safetyCounter = 1000;

        while (result.Count < 3 && safetyCounter-- > 0)
        {
            int value = GetWeightedRandom(entries);
            result.Add(value);

            // Sau khi có 3 giá trị, kiểm tra nếu tất cả đều trùng
            if (result.Count == 3)
            {
                if (result[0] == result[1] && result[1] == result[2])
                {
                    // Xóa giá trị cuối và thử lại
                    result.RemoveAt(2);
                }
            }
        }

        return result;
    }

    public static List<int> GetWeightedRandomDistinct(List<RandomWeightEntry> entries, int count)
    {
        HashSet<int> selectedValues = new HashSet<int>();

        int maxPossible = 0;
        foreach (var entry in entries)
            maxPossible += entry.values.Count;

        if (count > maxPossible)
        {
            Debug.LogError($"[RandomUtility] Không thể chọn {count} giá trị khác nhau vì tổng số giá trị là {maxPossible}");
            return new List<int>();
        }

        int safetyCounter = 1000;
        while (selectedValues.Count < count && safetyCounter-- > 0)
        {
            int value = GetWeightedRandom(entries);
            selectedValues.Add(value);
        }

        return new List<int>(selectedValues);
    }
}

[System.Serializable]
public struct RandomWeightEntry
{
    public List<int> values;   // Danh sách các giá trị (ví dụ: -1 hoặc [1,2,3])
    public float weight;       // Tỷ lệ xuất hiện (từ 0 đến 1)

    public RandomWeightEntry(List<int> values, float weight)
    {
        this.values = values;
        this.weight = weight;
    }
}
