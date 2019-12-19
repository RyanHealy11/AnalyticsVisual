using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntEvent
{

    public string eventName;
    public List<int> values = new List<int>();

    public IntEvent(string name)
    {
        eventName = name;
    }
    public IntEvent(string name, int val)
    {
        eventName = name;
        values.Add(val);
    }
}
