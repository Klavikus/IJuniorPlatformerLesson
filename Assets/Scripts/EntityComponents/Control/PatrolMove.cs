using UnityEngine;

namespace EntityComponents.Control
{
    public class PatrolMove : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _minTransitionDistance;

        private int _currentPointId;
        private float _distanceToTarget;

        public Transform CurrentTarget => _points[_currentPointId];

        private void Update()
        {
            _distanceToTarget = (_points[_currentPointId].position - transform.position).magnitude;

            if (_distanceToTarget <= _minTransitionDistance)
            {
                _currentPointId++;

                if (_currentPointId == _points.Length)
                    _currentPointId = 0;
            }
        }
    }
}