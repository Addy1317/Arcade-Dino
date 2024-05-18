using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlowpokeStudio.ArcadeDino
{
    public class PlayerController : MonoBehaviour
    {
        private CharacterController _characterController;
        private Vector3 _direction;

        [SerializeField] private float _gravity = 9.18f * 2f;
        [SerializeField] private float _jumpForce = 8f;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            _direction = Vector3.zero;
        }

        private void Update()
        {
            PlayerDirection();
        }

        private void PlayerDirection()
        {
            _direction += Vector3.down * _gravity * Time.deltaTime;

            if (_characterController.isGrounded)
            {
                _direction = Vector3.down;
                if( Input.GetButton("Jump"))
                {
                    _direction = Vector3.up * _jumpForce;
                }

            }
            _characterController.Move(_direction * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Obstacle"))
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}