using UnityEngine;

public class CashedMonoBehaviour : MonoBehaviour
{
    private Transform _transform;

    public Transform Transform
    {
        get
        {
            if (!_transform)
            {
                _transform = transform;
            }

            return _transform;
        }
    }
}