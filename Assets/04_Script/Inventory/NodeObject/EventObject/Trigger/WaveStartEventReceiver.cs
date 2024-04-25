using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStartEventReceiver : InventoryEventReceiverBase
{
    public GeneratorID generatorID;

    protected override void OnInit()
    {
        if (PlayerController.EventController != null)
        {

            PlayerController.EventController.OnWaveStart += HandleWaveStart;

        }

    }

    [BindExecuteType(typeof(SendData))]
    public override void GetSignal(object parm)
    {

        base.GetSignal(parm);

    }

    private void HandleWaveStart()
    {

        SendData s = new SendData(generatorID);

        GetSignal(s);

    }

    public override void Dispose()
    {
        if (PlayerController.EventController != null)
        {
            PlayerController.EventController.OnWaveStart -= HandleWaveStart;

        }

    }
}