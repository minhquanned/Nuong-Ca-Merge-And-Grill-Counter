using System;
using UnityEngine;

public class GameEventManager : MonoSingleton<GameEventManager>
{
    private GameEventObserver<GameEventKey> _eventObserver = new();

    public void Subscribe<T>(GameEventKey key, Action<T> callback)
    {
        _eventObserver.Subscribe(key, callback);
    }

    public void Unsubscribe<T>(GameEventKey key, Action<T> callback)
    {
        _eventObserver.Unsubscribe(key, callback);
    }

    public void Notify<T>(GameEventKey key, T data)
    {
        _eventObserver.Notify(key, data);
    }
}

public enum GameEventKey
{
    OnInitMatrixDone,
    OnMatrixRandomNextItem,
    OnMatch3Item,
}

public struct Match3ItemData
{
    public int row;
    public int col;
    public int id;
}

public struct MatrixRandomNextItem
{
    public int row;
    public int col;
}