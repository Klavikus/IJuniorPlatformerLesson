using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _defaultZOffset = -10;

    private void Update()
    {
        if (_target == null)
            return;
        
        gameObject.transform.position = new Vector3(_target.position.x, _target.position.y, _defaultZOffset);
    }
}