using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobController : DataController<Job>
{
    protected override Job[] Datas => CleaningDatas.Instance.jobs;
}
