using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatEvent
{

    public string eventName;
    public List<float> values = new List<float>();


    public FloatEvent(string name)
    {
        eventName = name;
    }
    public FloatEvent(string name, float val)
    {
        eventName = name;
        values.Add(val);
    }
}
