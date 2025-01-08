#if UNITY_EDITOR
using UnityEngine;

public sealed class GameTester : MonoBehaviour
{
    public static bool CantSpawning;

    [SerializeField] bool _cantSpawning = true;
    [SerializeField] Health playerHealth;
    [SerializeField] ComplexitySettingsInProcedure procedureComplexitySettings;
    [SerializeField] DirectorObstaclesFabric obstaclesDirector;

    private void Awake()
    {
        CantSpawning = _cantSpawning;

        if(!Application.isEditor)
            Destroy(gameObject);
    }

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
        CantSpawning = true;
        obstaclesDirector.StopProcedureSpawningObstacles();
    }

    internal void StartSpawningObstacles()
    {
        CantSpawning = false;
        obstaclesDirector.StartProcedureSpawnignObstacles();
    }
    #endregion


}
#endif
