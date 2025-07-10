using System;
using UnityEngine;
using static Define;

public class InputManager
{
    public event Action<EInputType> OnInputEvent;

    public void UpdateInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnInputEvent?.Invoke(EInputType.Up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnInputEvent?.Invoke(EInputType.Down);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnInputEvent?.Invoke(EInputType.Left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnInputEvent?.Invoke(EInputType.Right);
        }
    }
}
