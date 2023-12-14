using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TwoDirInput(Vector2 dir);
public delegate void OneDirInput(float value);

public class PlayerInputController : IDisposable
{

    public Vector2 MoveDir { get; private set; }

    private void CheckMovementKeyInput()
    {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        MoveDir = new Vector2(x, y).normalized;

    }

    public void Update()
    {

        CheckMovementKeyInput();

    }

    public void Dispose()
    {

        //���߿� �̹�Ʈ �߰��ϸ� ���⼭ null�� �о��ֱ�

    }

}
