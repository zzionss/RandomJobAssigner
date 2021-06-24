using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobController : DataController<Job>
{
    protected override List<Job> DataList => DataSetting.Instance.JobList;
}
