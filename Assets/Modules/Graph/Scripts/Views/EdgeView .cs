using UnityEngine;

public class EdgeView : CashedMonoBehaviour
{
    public const float ModelScale = 0.1f;

    public void SetShape(float distance)
    {
        Transform.localScale = new Vector3(1f, 1f, distance);
    }
}