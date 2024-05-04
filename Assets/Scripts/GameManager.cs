using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlowpokeStudio.ArcadeDino
{
    public class GameManager : MonoBehaviour
    {
        //Basic Singleton
        internal static GameManager Instance { get; private set; }

        [SerializeField] private float initalGameSpeed = 5f;
        [SerializeField] private float gameSpeedIncreases = 0.1f;
        internal float gameSpeed {get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }

        private void OnDestroy()
        {
            if(Instance == this)
            {
                Instance = null;
            }
        }

        private void Start()
        {
            NewGame();
        }

        private void Update()
        {
            gameSpeed += gameSpeedIncreases * Time.deltaTime;
        }

        private void NewGame()
        {
            gameSpeed = initalGameSpeed;
        }

        
    }
}