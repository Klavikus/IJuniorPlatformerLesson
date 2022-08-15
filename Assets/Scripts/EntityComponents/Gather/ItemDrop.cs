using EntityComponents.Attack;
using UnityEngine;

namespace EntityComponents.Gather
{
    public class ItemDrop : MonoBehaviour
    {
        [SerializeField] private DamageHandler _damageHandler;
        [SerializeField] private Transform _dropAnchor;
        [SerializeField] private GameObject _itemPrefab;

        private void OnEnable() => _damageHandler.Died += OnDied;

        private void OnDisable() => _damageHandler.Died -= OnDied;

        private void OnDied() => Instantiate(_itemPrefab, _dropAnchor.position, Quaternion.identity);
    }
}