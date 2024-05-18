using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace SlowpokeStudio.ArcadeDino
{
    public class GameManager : MonoBehaviour
    {
        //Basic Singleton
        internal static GameManager Instance { get; private set; }

        [SerializeField] private float _initalGameSpeed = 5f;
        [SerializeField] private float _gameSpeedIncreases = 0.1f;

        [SerializeField] private TextMeshProUGUI _gameOverText;
        [SerializeField] private Button _retryButton;

        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _hiScoreText;
        internal float gameSpeed {get; private set; }

        private PlayerController _playerController;
        private Spawner _spawner;

        private float _score;


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
            _score += gameSpeed * Time.deltaTime;
            _scoreText.text = Mathf.FloorToInt(_score).ToString("D5");
        }

        public void NewGame()
        {
            Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

            foreach (var obstacle in obstacles)
            {
                Destroy(obstacle.gameObject);
            }

            _score = 0f;
            gameSpeed = _initalGameSpeed;
         
            enabled = true;

            _playerController.gameObject.SetActive(true);
            _spawner.gameObject.SetActive(true);
            _gameOverText.gameObject.SetActive(false);
            _retryButton.gameObject.SetActive(false);
            UpdateHiScore();
        }

        public void GameOver()
        {
            gameSpeed = 0f;
            enabled = false;
            _playerController.gameObject.SetActive(false);
            _spawner.gameObject.SetActive(false);
            _gameOverText.gameObject.SetActive(true);
            _retryButton.gameObject.SetActive(true);
            UpdateHiScore();
        }
        
        private void UpdateHiScore()
        {
            float hiscore = PlayerPrefs.GetFloat("hiscore", 0);
             
            if(_score > hiscore)
            {
                hiscore = _score;
                PlayerPrefs.SetFloat("hiscore", hiscore);
            }

            _hiScoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");

        }

    }
}