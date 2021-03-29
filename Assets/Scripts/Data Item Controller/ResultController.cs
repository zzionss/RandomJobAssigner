using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Random = UnityEngine.Random;

public class ResultController : DataController<Result>
{
    protected override Result[] Datas => GetResults();

    protected override void Awake()
    {

    }

    private Result[] GetResults()
    {
        Debug.Log(CleaningDatas.Instance.ActivePersons.Length);
        Debug.Log(CleaningDatas.Instance.JobCountSum);

        Debug.Assert(CleaningDatas.Instance.ActivePersons.Length == CleaningDatas.Instance.JobCountSum);

        Result[] results = new Result[CleaningDatas.Instance.ActivePersons.Length];

        string[] radomizedJobNames = RandomizeJobNames();

        for(int i = 0; i < results.Length; i++)
        {
            Debug.Log(radomizedJobNames[i]);
            Debug.Log(CleaningDatas.Instance.ActivePersons[i].name);

            results[i] = new Result
            {
                name = CleaningDatas.Instance.ActivePersons[i].name,
                job = radomizedJobNames[i]
            };
        }

        return results;
    }

    private string[] RandomizeJobNames()
    {
        string[] jobNames = new string[CleaningDatas.Instance.JobCountSum];

        int currentPosition = 0;
        foreach(Job job in CleaningDatas.Instance.jobs)
        {
            Array.Copy(Enumerable.Repeat(job.name, job.count).ToArray(), 0, jobNames, currentPosition, job.count);
            currentPosition += job.count;
        }

        return jobNames.OrderBy(x => Random.Range(0, 10)).ToArray();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            CreateItems();
            ClipboardController.Instance.Capture();
        }
    }
}
