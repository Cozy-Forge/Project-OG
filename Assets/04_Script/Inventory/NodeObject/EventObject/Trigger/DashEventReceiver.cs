using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using System;
public class DashEventReceiver : InventoryEventReceiverBase
{
    public GeneratorID generatorID;

    protected override void OnInit()
    {
        if (PlayerController.EventController != null)
        {

            PlayerController.EventController.OnDash += HandleDash;

        }

    }

    [BindExecuteType(typeof(SendData))]
    public override void GetSignal(object parm)
    {

        base.GetSignal(parm);

    }

    private void HandleDash()
    {
        SendData s = new SendData(generatorID, transform,TriggerID.Dash);

        GetSignal(s);

    }

    public override void Dispose()
    {
        if (PlayerController.EventController != null)
        {
            PlayerController.EventController.OnDash -= HandleDash;

        }

    }
}
