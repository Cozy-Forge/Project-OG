using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public delegate void ReceiveEvent(object parm = null);

// �̺�Ʈ�� ���� ������ �۵��ϴ� �� ex) �뽬
public abstract class InventoryEventReceiverBase : InventoryObjectRoot, IDisposable
{

    public virtual void Dispose()
    {

    }

    public override void GetSignal(object parm)
    {

        foreach(var item in connectedOutput)
        {

            item.DoGetSignal(parm);

        }

    }

}