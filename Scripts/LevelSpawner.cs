using System;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _template; 
    [SerializeField] private GameObject _templateEmpty;
    [SerializeField] private GameObject _spawnTo;
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private BirdController _birdController;
    
    private float _distanceTraveled = 0;
    private bool _isFirst = true;
    private GameObject _firstEmpty;
    private GameController _gameController;

    private void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (_isFirst || (transform.position.x - _distanceTraveled >= 1.79f && !_birdController.Played))
        {
            _firstEmpty = Instantiate(_templateEmpty, _spawnTo.transform);
            _isFirst = false;
            Destroy(_firstEmpty, 10f);
            _distanceTraveled = transform.position.x;
            return;
        }
        if (_gameController.GameOver) return;
        if (_firstEmpty)
        {
            _firstEmpty.transform.parent = transform;
        }

        transform.position += new Vector3(_speed * Time.deltaTime, 0, 0);
        if (transform.position.x - _distanceTraveled >= 1.79f && _birdController.Played)
        {
            _distanceTraveled = transform.position.x;
            GameObject spawned = Instantiate(_template, _spawnTo.transform);
            spawned.transform.parent = transform;
            Destroy(spawned, 20f);
        }
    }
}
