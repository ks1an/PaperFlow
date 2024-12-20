using UnityEngine;

public sealed class GameUI : MonoBehaviour
{
    [Header("GameUI")]
    [SerializeField] GameObject _staminaBar;
    [SerializeField] GameObject _ballIndicator;

    void EnableStaminaBarAndBallIndicator()
    {
        _staminaBar.SetActive(true);
        _ballIndicator.SetActive(true);
        Debug.Log("Enable");
    }

    void DisableStaminaBarAndBallIndicator()
    {
        _staminaBar.SetActive(false);
        _ballIndicator.SetActive(false);
        Debug.Log("Disable");
    }

    void OnEnable()
    {
        GameStateController.OnStartGameState += DisableStaminaBarAndBallIndicator;
        ComplexityController.OnPurchasedBallSkillAndStaminaBar += EnableStaminaBarAndBallIndicator;
    }
    void OnDisable()
    {
        ComplexityController.OnPurchasedBallSkillAndStaminaBar -= EnableStaminaBarAndBallIndicator;
        GameStateController.OnStartGameState -= DisableStaminaBarAndBallIndicator;
    }
}
