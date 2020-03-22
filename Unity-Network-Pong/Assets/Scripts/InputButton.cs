using UnityEngine;

public class InputButton : MonoBehaviour
{
    public static float VerticalInput;

    public enum State
    {
        None,
        Down,
        Up
    }

    private State state = State.None;

    // 현재 State에 따라 VerticalInput 값 지정
    private void Update()
    {
        if(state == State.None)
        {
            VerticalInput = 0f;
        }
        else if(state == State.Up)
        {
            VerticalInput = 1f;
        }
        else if(state == State.Down)
        {
            VerticalInput = -1f;
        }
    }

    public void OnMoveUpButtonPressed()
    {
        state = State.Up;
    }

    public void OnMoveUpButtonUp()
    {
        if(state == State.Up)
        {
            state = State.None;
        }
    }

    public void OnMoveDownButtonPressed()
    {
        state = State.Down;
    }

    public void OnMoveDownButtonUp()
    {
        if(state == State.Down)
        {
            state = State.None;
        }
    }
}