using UnityEngine;

public static class CachedMath
{
    #region Vectors

    public static Vector2 Vector2Up = Vector2.up;
    public static Vector2 Vector2Right = Vector2.right;
    public static Vector2 Vector2Down = Vector2.down;
    public static Vector2 Vector2Left = Vector2.left;
    public static Vector2 Vector2Zero = Vector2.zero;

    public static Vector2 Vector2_0_180 = new(0, 180);
    #endregion

    #region Quaternions

    public static Quaternion QuaternionIdentity = Quaternion.identity;
    #endregion
}
