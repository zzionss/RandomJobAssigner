using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataItem<T> : MonoBehaviour where T : Data
{
    public T data;

    public abstract void Initialize();
}
