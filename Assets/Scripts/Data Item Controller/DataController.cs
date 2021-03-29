using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataController<T> : MonoBehaviour where T : Data
{
    public GameObject itemPrefab;
    protected GameObject[] items;

    protected abstract T[] Datas { get;}

    protected virtual void Awake()
    {
        CreateItems();
    }

    public void CreateItems()
    {
        RemoveItems();

        T[] datas = Datas;
        items = new GameObject[datas.Length];

        for(int i = 0; i< datas.Length; i++)
        {
            GameObject instantiatedItem = Instantiate(itemPrefab, transform);
            instantiatedItem.GetComponent<DataItem<T>>().data = datas[i];
            instantiatedItem.GetComponent<DataItem<T>>().Initialize();
            items[i] = instantiatedItem;
        }
    }

    public void RemoveItems()
    {
        if(items == null)
        {
            return;
        }

        foreach(GameObject item in items)
        {
            Destroy(item);
        }
    }
}
