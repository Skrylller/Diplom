using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController main;

    [SerializeField] private float _speedX;
    [SerializeField] private float _speedY;
    private float _realVelocityX;
    private float _realVelocityY;

    private Rigidbody2D _playerRigidbody2D;
    private Vector2 _startPosition;

    private void Awake()
    {
        main = this;
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _startPosition = transform.position;
        RestartPosition();
    }

    private void Update()
    {
        CheckDammage();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            collision.gameObject.SetActive(false);
            MainStats.main.money.Add(1);
            UIController.main.UpdateMoneyText();
        }
    }

    private void CheckDammage()
    {
        if(GameplaySceneController.main.isStartGameplay && transform.position.y < -5f)
        {
            Dammage();
        }
    }

    private void MovePlayer()
    {
        if (transform.position.y < _startPosition.y)
            _realVelocityY = _speedY;
        else
            _realVelocityY = 0;

        _playerRigidbody2D.velocity = new Vector2(_realVelocityX, _realVelocityY);
    }

    private void Dammage()
    {
        GameplaySceneController.main.StopGameplay();
    }

    public void ChangePlayerPosition()
    {
        _realVelocityX = -_realVelocityX;
    }

    public void RestartPosition()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        _realVelocityX = _speedX;
    }
}
