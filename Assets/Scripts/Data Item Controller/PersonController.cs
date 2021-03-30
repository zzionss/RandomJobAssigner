using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : DataController<Person>
{
    protected override List<Person> DataList => CleaningDatas.Instance.PersonList;
}
