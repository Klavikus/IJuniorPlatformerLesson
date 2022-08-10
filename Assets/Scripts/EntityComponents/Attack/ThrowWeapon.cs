using Unity.Mathematics;
using UnityEngine;

namespace EntityComponents.Attack
{
    public class ThrowWeapon : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _damage;
        [SerializeField] private GameObject _hitVFX;
        [SerializeField] private float _radius;

        private LayerMask _whatAttack;
        private Collider2D[] _hits = new Collider2D[1];

        public void Init(LayerMask whatAttack)
        {
            _whatAttack = whatAttack;
            Destroy(gameObject, _lifeTime);
        }

        private void Update()
        {
            transform.position += transform.right * (_moveSpeed * Time.deltaTime);

            if (CheckHit())
            {
                foreach (Collider2D hit in _hits)
                {
                    if (hit.TryGetComponent(out IDamageable damageable))
                    {
                        damageable.ApplyDamage(_damage);
                    }
                }

                Instantiate(_hitVFX, transform.position, quaternion.identity);
                Destroy(gameObject);
            }
        }

        private bool CheckHit()
        {
            int hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _hits, _whatAttack);

            return hitCount > 0;
        }
    }
}