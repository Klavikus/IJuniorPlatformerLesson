using System;
using System.Threading.Tasks;
using EntityComponents.Attack;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoopController : MonoBehaviour
{
    [SerializeField] private DamageHandler _damageHandler;
    [SerializeField] private GameObject _deathView;
    [SerializeField] private int _restartDelay;
    private void OnEnable() => _damageHandler.Died += OnPlayerDied;

    private void OnDisable() => _damageHandler.Died -= OnPlayerDied;

    private async void OnPlayerDied()
    {
        _deathView.SetActive(true);
        await Task.Delay(new TimeSpan(hours: 0, minutes: 0, seconds: _restartDelay));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}