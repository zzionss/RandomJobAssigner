using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Random = UnityEngine.Random;

public class ResultController : DataController<Result>
{
    protected override List<Result> DataList => GetResults();

    private List<Result> GetResults()
    {
        Debug.Log(CleaningDatas.Instance.ActivePersons.Count);
        Debug.Log(CleaningDatas.Instance.JobCountSum);

        try
        {
            if(CleaningDatas.Instance.ActivePersons.Count != CleaningDatas.Instance.JobCountSum)
            {
                throw new Exception("인원을 맞춰주세요.");
            }
            List<Result> resultList = new List<Result>();

            string[] radomizedJobNames = RandomizeJobNames();

            for(int i = 0; i < CleaningDatas.Instance.JobCountSum; i++)
            {
                Debug.Log(radomizedJobNames[i]);
                Debug.Log(CleaningDatas.Instance.ActivePersons[i].name);

                resultList.Add(new Result
                {
                    name = CleaningDatas.Instance.ActivePersons[i].name,
                    job = radomizedJobNames[i]
                });
            }

            return resultList;
        }
        catch(Exception e)
        {
            Debug.LogWarning(e.Message);
            PopUp.ShowPopUp(e.Message);
            return null;
        }
    }

    private string[] RandomizeJobNames()
    {
        string[] jobNames = new string[CleaningDatas.Instance.JobCountSum];

        int currentPosition = 0;
        foreach(Job job in CleaningDatas.Instance.JobList)
        {
            Array.Copy(Enumerable.Repeat(job.name, job.count).ToArray(), 0, jobNames, currentPosition, job.count);
            currentPosition += job.count;
        }

        return jobNames.OrderBy(x => Random.Range(0, 10)).ToArray();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CreateAllItems();
        }
    }

    public override void CreateAllItems(List<Result> dataList)
    {
        RemoveAllItems();
        base.CreateAllItems(dataList);
        ClipboardController.Instance.Capture();
    }
}
