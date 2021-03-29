using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    private static object syncObject = new object();
    private static bool isAppClosing = false;

    public static T Instance
    {
        get
        {
            if(isAppClosing)
                return null;

            lock(syncObject)
            {
                string typeName = typeof(T).Name;

                if(instance == null)
                {
                    T[] objects = FindObjectsOfType<T>();

                    if(objects.Length <= 0)
                    {
                        GameObject gameObject = GameObject.Find(typeName);
                        if(gameObject == null)
                            gameObject = new GameObject(typeName);
                        instance = gameObject.AddComponent<T>();
                    }
                    else if(objects.Length <= 1)
                        instance = objects[0];
                    else
                    {
                        instance = objects[0];
                        Debug.LogError($"There is more than one {typeName} in the scene.");
                    }
                }
                return instance;
            }
        }
    }

    protected virtual void OnApplicationQuit()
    {
        isAppClosing = true;
    }
}
