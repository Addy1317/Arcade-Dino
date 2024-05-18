using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SlowpokeStudio.ArcadeDino
{
    public class GameManager : MonoBehaviour
    {
        //Basic Singleton
        internal static GameManager Instance { get; private set; }

        [SerializeField] private float _initalGameSpeed = 5f;
        [SerializeField] private float _gameSpeedIncreases = 0.1f;
        [SerializeField] private GameObject _gameOverPanel;
        internal float gameSpeed {get; private set; }

        private PlayerController _playerController;
        private Spawner _spawner;

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
            _playerController = FindObjectOfType<PlayerController>();
            _spawner = FindObjectOfType<Spawner>();
            NewGame();
        }

        private void Update()
        {
            gameSpeed += _gameSpeedIncreases * Time.deltaTime;
        }

        public void NewGame()
        {
            Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

            foreach (var obstacle in obstacles)
            {
                Destroy(obstacle.gameObject);
            }

            gameSpeed = _initalGameSpeed;
            enabled = true;

            _playerController.gameObject.SetActive(true);
            _spawner.gameObject.SetActive(true);
            _gameOverPanel.SetActive(false);
        }

        public void GameOver()
        {
            gameSpeed = 0f;
            enabled = false;
            _playerController.gameObject.SetActive(false);
            _spawner.gameObject.SetActive(false);
            _gameOverPanel.SetActive(true);
        }
        
    }
}