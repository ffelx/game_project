using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class BasketController : MonoBehaviour
{
    [SerializeField] private Transform[] lanes;
    private static Text _scoreText;
    public Text scoreText;

    [SerializeField] private int _maxScore = 100;


    private int currentLane = 1;
    private static int _score;
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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0)
        {
            currentLane--;
            MoveToLane();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < lanes.Length - 1)
        {
            currentLane++;
            MoveToLane();
        }
    }

    void MoveToLane()
    {
        transform.position = new Vector3(lanes[currentLane].position.x, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trash"))
        {
            _score += 1;
            if (_score >= _maxScore)
            {
                Win();
            }
            scoreText.text = $"Счет: {_score}/{_maxScore}";
            Debug.Log("2");
        }
    }

    private void Win()
    {
        Debug.Log("Победа");
    }
}
