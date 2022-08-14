using EntityComponents.Audio;
using TMPro;
using UnityEngine;
using AudioType = EntityComponents.Audio.AudioType;

namespace EntityComponents.Gather
{
    public class PickupHandler : MonoBehaviour
    {
        [SerializeField] private LayerMask _whatIsPickup;
        [SerializeField] private Transform _pickupAnchor;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Vector2 _pickupSize;
        [SerializeField] private AudioPlayer _audioPlayer;

        private int _coinsAmount;
        private Collider2D[] _hits = new Collider2D[1];

        private void Update()
        {
            if (Physics2D.OverlapBoxNonAlloc(_pickupAnchor.position, _pickupSize, angle: 0, _hits, _whatIsPickup) > 0)
            {
                if (_hits[0].TryGetComponent(out Coin coin))
                {
                    OnPickup();
                    Destroy(coin.gameObject);
                }
            }
        }

        private void OnPickup()

        {
            _audioPlayer.Play(AudioType.ItemPickup);
            _coinsAmount++;
            _text.text = _coinsAmount.ToString();
        }
    }
}