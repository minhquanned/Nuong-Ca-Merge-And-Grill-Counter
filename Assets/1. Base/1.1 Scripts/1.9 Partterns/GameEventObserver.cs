using System;
using System.Collections.Generic;

public class GameEventObserver<TKey> where TKey : Enum
{
    // Mỗi key lưu theo kiểu Dictionary<Type, List<Delegate>>
    private Dictionary<TKey, Dictionary<Type, List<Delegate>>> _eventTable = new();

    /// <summary>
    /// Đăng ký callback với key và kiểu dữ liệu cụ thể
    /// </summary>
    public void Subscribe<T>(TKey key, Action<T> callback)
    {
        var type = typeof(T);

        if (!_eventTable.ContainsKey(key))
        {
            _eventTable[key] = new Dictionary<Type, List<Delegate>>();
        }

        if (!_eventTable[key].ContainsKey(type))
        {
            _eventTable[key][type] = new List<Delegate>();
        }

        if (!_eventTable[key][type].Contains(callback))
        {
            _eventTable[key][type].Add(callback);
        }
    }

    /// <summary>
    /// Hủy đăng ký callback
    /// </summary>
    public void Unsubscribe<T>(TKey key, Action<T> callback)
    {
        var type = typeof(T);

        if (_eventTable.TryGetValue(key, out var typeMap))
        {
            if (typeMap.TryGetValue(type, out var delegateList))
            {
                delegateList.Remove(callback);
            }
        }
    }

    /// <summary>
    /// Gửi event với kiểu dữ liệu cụ thể
    /// </summary>
    public void Notify<T>(TKey key, T data)
    {
        var type = typeof(T);

        if (_eventTable.TryGetValue(key, out var typeMap))
        {
            if (typeMap.TryGetValue(type, out var delegateList))
            {
                foreach (var callback in delegateList)
                {
                    if (callback is Action<T> typedCallback)
                    {
                        typedCallback.Invoke(data);
                    }
                }
            }
        }
    }
}
