using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlowpokeStudio.ArcadeDino
{
    public class Ground : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            GroundScrolling();
        }

        private void GroundScrolling()
        {
            float speed = GameManager.Instance.gameSpeed / transform.localScale.x;
            _meshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
        }
    }
}