using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlowpokeStudio.ArcadeDino
{
    public class AnimatedSprite : MonoBehaviour
    {
        [SerializeField] private Sprite[] _sprites;
        private SpriteRenderer _spriteRenderer;
        private int _frame;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            Invoke(nameof(Animate),0f);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        private void Animate()
        {
            _frame++;

            if(_frame >= _sprites.Length)
            {
               _frame = 0;
            }

            if(_frame >= 0 && _frame < _sprites.Length)
            {
                _spriteRenderer.sprite = _sprites[_frame];
            }

            Invoke(nameof(Animate),1f/GameManager.Instance.gameSpeed);
        }
    }
}