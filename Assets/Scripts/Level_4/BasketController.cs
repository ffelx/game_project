using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class BasketController : MonoBehaviour
{

    private bool _isWin = false;
    [SerializeField] private Transform[] _lanes;
    private static Text _scoreText;
    public Text scoreText;

    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private GameObject _game;
    [SerializeField] private GameObject _gameCanvas;
    [SerializeField] private GameObject _gameCanvas2;

    [SerializeField] private int _maxScore = 100;
    [SerializeField] private GameStarter _gameStarter;

    private int currentLane = 1;

    private static int _score;
    private static int _trashCollected;

    public static int Score 
    {
        get => _score;
        set
        {
            _score = value;
            _scoreText.text = $"Счет: {_score}/100";

        }
    }

    private void Start()
    {
        _scoreText = scoreText;
        _audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) 
          || Input.GetKeyDown(KeyCode.A)) 
          && currentLane > 0)
        {
            MoveLeft();
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) 
               || Input.GetKeyDown(KeyCode.D)) 
               && currentLane < _lanes.Length - 1)
        {
            MoveRight();
        }
    }

    public void MoveLeft()
    {
        if (currentLane > 0)
        {
            currentLane--;
            MoveToLane();
        }
    }

    public void MoveRight()
    {
        if (currentLane < _lanes.Length - 1)
        {
            currentLane++;
            MoveToLane();
        }
    }

    void MoveToLane()
    {
        transform.position = new Vector3(_lanes[currentLane].position.x, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trash"))
        {

            CollectTrash();
        }
    }

    public void CollectTrash()
    {
        if (!_isWin)
        {
            _audioManager.PlayNetClickSound();
        }
        _score += 1;
        _trashCollected += 1;
        if (_score >= _maxScore)
        {
            Win();
        }
        scoreText.text = $"Счет: {_score}/{_maxScore}";
        Debug.Log("2");

        if (_trashCollected == 1 || _trashCollected % 25 == 0)
        {
            var helper = FindObjectOfType<Helper>();
            helper.ShowGoodDialog();
        }
    }

    private void Win()
    {
        if (!_isWin)
        {
            _isWin = true;
            _gameCanvas.SetActive(false);
            _gameCanvas2.SetActive(false);
            _gameStarter.CloseGame();

            Debug.Log("Победа");
        }
        
    }
}
