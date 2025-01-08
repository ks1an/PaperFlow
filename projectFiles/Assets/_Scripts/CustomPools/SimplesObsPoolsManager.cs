using UnityEngine;

public class SimplesObsPoolsManager : MonoBehaviour
{
    #region CellingLamp

    [Header("CellingLamp")]
    public SimpleObstaclePool cellingLampPool;
    [SerializeField] SimpleObstacle cellingLampPrefab;
    [SerializeField] int cellingLampStartCountInPool;
    #endregion

    private void Awake()
    {
        cellingLampPool = new SimpleObstaclePool(cellingLampPrefab, cellingLampStartCountInPool);
    }
}
