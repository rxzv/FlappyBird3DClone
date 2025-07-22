using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 1.5f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _cooldownTime = 0.1f;
    [SerializeField] private Text _scoreText;
    [SerializeField] private AudioClip _flySound;
    [SerializeField] private AudioClip _scoreSound;
    [SerializeField] private AudioClip _dieSound;
    [SerializeField] private AudioClip _ponkSound;
    [SerializeField] private GameObject _getReadyText;
    
    private AudioSource _audioSource;
    private int _speed;
    private Rigidbody _rigidbody;
    private bool _cooldown = false;
    private bool _isDie = false;
    public bool Played {get; private set;}
    public int Score {get; private set;}
    public bool GameOver { get; private set; }
    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        GameOver = false;
        Score = 0;
        Played = false;
    }

    private void Update()
    {
        if (!Played && Input.GetKeyDown(KeyCode.Space))
        {
            _getReadyText.SetActive(false);
            _audioSource.PlayOneShot(_flySound);
            Played = true;
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionX | 
                                     RigidbodyConstraints.FreezePositionZ | 
                                     RigidbodyConstraints.FreezeRotationY | 
                                     RigidbodyConstraints.FreezeRotationZ;
            StartCoroutine(CooldownRefresh());
            _rigidbody.linearVelocity = Vector2.up * _jumpForce;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _cooldown == false)
        {
            _audioSource.PlayOneShot(_flySound);
            _cooldown = true;
            StartCoroutine(CooldownRefresh());
            _rigidbody.linearVelocity = Vector2.up * _jumpForce;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_isDie) return;
        if (other.tag == "Score")
        {
            _audioSource.PlayOneShot(_scoreSound);
            Score++;
            _scoreText.text = Score.ToString();
        }

        if (other.tag == "Wall")
        {
            _audioSource.PlayOneShot(_ponkSound);
            Debug.Log("Game Over!");
            GameOver = true;
            StartCoroutine(DiePlayOneShot());
            gameObject.GetComponent<BirdController>().enabled = false;
            _isDie = true;
        }
    }

    private IEnumerator DiePlayOneShot()
    {
        yield return new WaitForSeconds(0.1f);
        _audioSource.PlayOneShot(_dieSound);
    }
    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(_rigidbody.linearVelocity.y * _rotationSpeed, 90f, 0f);
    }

    private IEnumerator CooldownRefresh()
    {
        yield return new WaitForSeconds(_cooldownTime);
        _cooldown = false;
    }
}
