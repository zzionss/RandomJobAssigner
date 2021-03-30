using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataItem<T> : MonoBehaviour where T : Data, new()
{
    public T Data { get; set; }
    public DataController<T> DataController { get; set; }

    public abstract void Initialize();

    public void DeleteItem()
    {
        DataController.RemoveItem(Data);
    }
}
