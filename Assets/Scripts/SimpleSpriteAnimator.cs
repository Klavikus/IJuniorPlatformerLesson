using System.Collections;
using UnityEngine;

public class SimpleSpriteAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _delay;
    [SerializeField] private bool _destroyAfter;

    private IEnumerator Start()
    {
        int frameId = 0;

        while (true)
        {
            _renderer.sprite = _sprites[frameId % _sprites.Length];
            
            yield return new WaitForSeconds(_delay);
           
            frameId++;

            if (frameId == _sprites.Length && _destroyAfter) 
                Destroy(gameObject);
        }
    }
}