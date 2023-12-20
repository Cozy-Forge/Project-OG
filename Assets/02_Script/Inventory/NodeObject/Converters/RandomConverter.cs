using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomConverter : InventoryConverterBase
{

    public enum RandomConverterRunningType
    {

        Ignore,
        Multiply,
        Add,
        Subtract

    }

    public float min, max;
    public RandomConverterRunningType runningType;

    [BindExecuteType(typeof(float))]
    public override void GetSignal([BindParameterType(typeof(float))]object signal)
    {

        float value = runningType switch
        {

            RandomConverterRunningType.Ignore => Random.Range(min, max),
            RandomConverterRunningType.Multiply => (float)signal * Random.Range(min, max),
            RandomConverterRunningType.Add => (float)signal + Random.Range(min, max),
            RandomConverterRunningType.Subtract => (float)signal - Random.Range(min, max),
            _ => 0

        };

        foreach(var item in connects)
        {

            item.DoGetSignal(value);

        }

    }

}
