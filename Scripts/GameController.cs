using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image _medalImg;
    [SerializeField] private GameObject _bird;
    [SerializeField] private BirdController _birdController;
    [SerializeField] private LevelSpawner _levelSpawner;
    
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _gameOverScreen;
    
    [SerializeField] private Text _resultScoreText;
    [SerializeField] private Text _resultBestScoreText;
    [SerializeField] private Text _scoreText;

    private int _score = 0;
    private int _scoreBest = 0;

    private Data _data;
    
    public bool GameOver { get; private set; }

    private void Start()
    {
        _data = Data.Instance;
    }

    private void Update()
    {
        if (!_birdController.GameOver) return;
        if (_birdController.GameOver)
        {
            _bird.GetComponent<Animator>().enabled = false;
            _birdController.enabled = false;
            _levelSpawner.enabled = false;
            _gameScreen.SetActive(false);
            _gameOverScreen.SetActive(true);
            Invoke("ReloadGameScene", 8f);
        }
        for (; _score < int.Parse(_scoreText.text);)
        {
            ScorePlus();
        }

        if (_data.BestScore >= int.Parse(_scoreText.text))
        {
            for (; _scoreBest < int.Parse(_scoreText.text);)
            {
                ScoreBestPlus();
            }
        }
        else if (_data.BestScore < int.Parse(_scoreText.text))
        {
            for (; _score < int.Parse(_scoreText.text);)
            {
                ScorePlus();
            }
            _data.BestScore = int.Parse(_scoreText.text);
        }
    }
    private void ScoreBestPlus()
    {
        _scoreBest++;
        _resultBestScoreText.text = _scoreBest.ToString();
    }
    private void ScorePlus()
    {
        _score++;
        _resultScoreText.text = _score.ToString();
    }

    public void ReloadGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void MedalChecker()
    {
        switch (int.Parse(_scoreText.text))
        {
            case 10-20:
                _medalImg.sprite = sprites[0];
                break;
            case 21-30:
                _medalImg.sprite = sprites[1];
                break;
            case 31-50:
                _medalImg.sprite = sprites[2];
                break;
            case 50:
                _medalImg.sprite = sprites[3];
                break;
            default:
                _medalImg.enabled = false;
                break;
        }
    }
}
