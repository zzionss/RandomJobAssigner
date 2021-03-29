using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : DataController<Person>
{
    protected override Person[] Datas => CleaningDatas.Instance.persons;
}
