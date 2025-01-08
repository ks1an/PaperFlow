#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameTester))]
public sealed class GameTestInspectorHelper : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(10);
        GameTester tester = (GameTester)target;

        #region Player
        GUILayout.Label("Player");

        #region Stamina&skills

        if (GUILayout.Button("Obtain Ball skill"))
            tester.ObstainBallSkill();

        if (GUILayout.Button("AddComplexityStaminaForPlayers"))
            tester.AddStamina();

        #endregion

        GUILayout.Space(10);

        #region Health

        if (GUILayout.Button("Heal 1 HP"))
            tester.HealPlayerOneHP();

        if (GUILayout.Button("Heal max HP"))
            tester.HealPlayerMaxHP();

        if (GUILayout.Button("Take max Damage"))
            tester.TakeMaxPlayerDamage();

        #endregion

        #endregion

        #region Complexity
        GUILayout.Space(10);
        GUILayout.Label("Complexity");

        if (GUILayout.Button("GoNextComplexity"))
            tester.GoNextComplexity();

        #endregion

        #region Obstacles
        GUILayout.Space(10);
        GUILayout.Label("Obstacles");

        if (GUILayout.Button("StopSpawning"))
            tester.StopSpawningObstacles();

        if (GUILayout.Button("StartSpawning"))
            tester.StartSpawningObstacles();

        #endregion
    }
}
#endif
