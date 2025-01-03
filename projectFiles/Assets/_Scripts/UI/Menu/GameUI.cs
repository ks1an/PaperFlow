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
    }

    void DisableStaminaBarAndBallIndicator()
    {
        _staminaBar.SetActive(false);
        _ballIndicator.SetActive(false);
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
