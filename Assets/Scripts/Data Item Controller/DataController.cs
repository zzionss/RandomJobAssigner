using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataController<T> : MonoBehaviour where T : Data, new()
{
    public GameObject itemPrefab;
    protected List<GameObject> itemList;

    protected abstract List<T> DataList { get;}

    protected virtual void Awake()
    {
        CreateAllItems();
    }

    public void CreateAllItems() => CreateAllItems(DataList);
  
    public virtual void CreateAllItems(List<T> dataList)
    {
        if(dataList == null)
        {
            return;
        }

        itemList = new List<GameObject>();

        for(int i = 0; i< dataList.Count; i++)
        {
            CreateItem(dataList[i]);
        }
    }

    public void CreateNewItem() => CreateNewItem(new T());

    public void CreateNewItem(T data)
    {
        CreateItem(data);

        DataList.Add(data);
        CleaningDatas.Instance.Save();
    }

    public void RemoveAllItems()
    {
        if(itemList == null)
        {
            return;
        }

        foreach(GameObject item in itemList)
        {
            Destroy(item);
        }

        itemList = null;
    }

    public void RemoveItem(T data)
    {
        GameObject targetItem = itemList.Find(o => o.GetComponent<DataItem<T>>().Data == data);
        itemList.Remove(targetItem);
        Destroy(targetItem);

        DataList.Remove(data);
        CleaningDatas.Instance.Save();
    }

    private void CreateItem(T data)
    {
        GameObject instantiatedItem = Instantiate(itemPrefab, transform);
        instantiatedItem.GetComponent<DataItem<T>>().Data = data;
        instantiatedItem.GetComponent<DataItem<T>>().DataController = this;
        instantiatedItem.GetComponent<DataItem<T>>().Initialize();
        itemList.Add(instantiatedItem);
    }
}
