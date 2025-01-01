using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public int UpInput { get; private set; }
    public bool AccelerInput { get; private set; }
    public bool BallSkillInput { get; private set; }


    public void OnUpInput(InputAction.CallbackContext context)
    {
        UpInput = (int)(context.ReadValue<Vector2>() * CachedMath.Vector2Up).normalized.y;
    }

    public void OnAccelerInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AccelerInput = true;
        }

        if (context.canceled)
        {
            AccelerInput = false;
        }
    }

    #region BallSkill

    public void OnBallSkillInput(InputAction.CallbackContext context) 
    {
        if (context.started)
        {
            BallSkillInput = true;
        }

        if (context.canceled)
        {
            BallSkillInput = false;
        }
    }

    public void UseBallSkill() => BallSkillInput = false;
    #endregion
}
