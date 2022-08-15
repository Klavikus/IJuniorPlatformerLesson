using EntityComponents.Attack;
using UnityEngine;

namespace View
{
   public class UIController : MonoBehaviour
   {
      [SerializeField] private DamageHandler _damageHandler;
      [SerializeField] private BarView _healthBar;
   
      private void OnEnable() => _damageHandler.HealthChanged += _healthBar.OnValueChanged;

      private void OnDisable() => _damageHandler.HealthChanged -= _healthBar.OnValueChanged;
   }
}
