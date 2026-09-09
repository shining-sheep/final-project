using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Stat
{
    [SerializeField] private float baseValue;
    [SerializeField] private List<StatModifier> modifiers = new List<StatModifier>();

    private float finalValue;
    private bool needToCalcualte = true;

    public float GetValue()
    {
        if (needToCalcualte)
        {
            finalValue = GetFinalValue();
            needToCalcualte = false;
        }
        return finalValue ;
    }

    public void AddModifier(float value,string source)
    {
        StatModifier modToAdd = new StatModifier(value, source);
        modifiers.Add(modToAdd);
        needToCalcualte = true;
    }

    public void RemoveModidier(string source)
    {
        modifiers.RemoveAll(modifiers => modifiers.source == source);
        needToCalcualte = true;
    }

    private float GetFinalValue()
    {
        float finalValue = baseValue;

        foreach(var modifier in modifiers)
        {
            finalValue = finalValue + modifier.value;
        }
        return finalValue;
    }
}

[Serializable]

public class StatModifier{
    public float value;
    public string source;

    public StatModifier(float value, string source)
        {
            this.value = value;
            this.source = source;
        }
}

