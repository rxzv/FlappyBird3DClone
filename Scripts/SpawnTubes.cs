using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnTubes : MonoBehaviour
{
    [SerializeField] private GameObject _tubes;
    private GameController _gameController;

    private void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        if (_gameController.GameOver) return;
        var obj1 = Instantiate(_tubes, new Vector3(-1f, Random.Range(-0.15f, 0.2f), 0.17f), Quaternion.identity, transform.parent);
        var obj2 = Instantiate(_tubes, new Vector3(-1.35f, Random.Range(-0.15f, 0.2f), 0.17f), Quaternion.identity, transform.parent);
        var obj3 = Instantiate(_tubes, new Vector3(-1.7f, Random.Range(-0.15f, 0.2f), 0.17f), Quaternion.identity, transform.parent);
        var obj4 = Instantiate(_tubes, new Vector3(-2.05f, Random.Range(-0.15f, 0.2f), 0.17f), Quaternion.identity, transform.parent);
        var obj5 = Instantiate(_tubes, new Vector3(-2.4f, Random.Range(-0.15f, 0.2f), 0.17f), Quaternion.identity, transform.parent);
        Destroy(obj1, 20f);
        Destroy(obj2, 20f);
        Destroy(obj3, 20f);
        Destroy(obj4, 20f);
        Destroy(obj5, 20f);
    }
}
