using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Job : Data
{
    public string name;
    public int count;

    public Job()
    {
        name = "¿Ã∏ß";
        count = 0;
    }

    public Job(string name, int count)
    {
        this.name = name ?? throw new ArgumentNullException(nameof(name));
        this.count = count;
    }
}
