using UnityEngine;

public sealed class GameTester : MonoBehaviour
{
    private void Awake()
    {
        if(!Application.isEditor)
            Destroy(gameObject);
    }

#if UNITY_EDITOR
    [SerializeField] Health playerHealth;
    [SerializeField] ComplexitySettingsInProcedure procedureComplexitySettings;
    [SerializeField] DirectorObstaclesFabric obstaclesDirector;

    #region Stamina&Skill

    internal void AddStamina() => procedureComplexitySettings.InvokeOnComplexityAddedStamina();
    internal void ObstainBallSkill() => procedureComplexitySettings.InvokeOnObtainedBallSkillAndStaminaBar();
    #endregion

    #region Health

    internal void HealPlayerOneHP() => playerHealth.Heal(1);
    internal void HealPlayerMaxHP() => playerHealth.HealthToMax();
    internal void TakeMaxPlayerDamage() => playerHealth.TakeMaxDamage();
    #endregion

    #region Complexity

    internal void GoNextComplexity() => procedureComplexitySettings.CheckNeddObtaclesForNextComplexity(100000000);
    #endregion

    #region Obstacles

    internal void StopSpawningObstacles()
    {
        obstaclesDirector.StopSpawning();
    }

    internal void StartSpawningObstacles()
    {
        obstaclesDirector.StartSpawning();
    }
    #endregion
#endif
}
