using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySceneController : MonoBehaviour
{
    public static GameplaySceneController main;

    public const string bestResultKey = "bestRes";

    [SerializeField] private float _sceneUpperBoundary;
    [SerializeField] private float _sceneDownerBoundary;
    [SerializeField] private float _sceneSpeed;
    public float sceneSpeed 
    { 
        get 
        { 
            return _sceneSpeed; 
        } 

        private set 
        { 
            _sceneSpeed = value; 
        }
    }
    [SerializeField] private float _sceneSlowlySpeed;

    [SerializeField] private int _costContinueGame = 10;
    public int costContinueGame
    {
        get
        {
            return _costContinueGame;
        }

        private set
        {
            _costContinueGame = value;
        }
    }

    public bool isStartGameplay { get; private set; }
    public float startTime { get; private set; }
    public int numDeath { get; private set; }

    private void Awake()
    {
        main = this;
    }

    private void Update()
    {
        if (isStartGameplay)
        {
            SetScore();
        }
    }

    private void SetScore()
    {
        UIController.main.SetScoreText((int)(Time.time - startTime));
    }

    public float ReturnUpBoundary()
    {
        return _sceneUpperBoundary;
    }

    public float ReturnDownBoundary()
    {
        return _sceneDownerBoundary;
    }

    public void RestartLevel()
    {
        Time.timeScale = 0;
        PlayerController.main.RestartPosition();
        SceneGenerator.main.DestroyAll();
    }

    public void CountineLevel()
    {
            Time.timeScale = 1;
            isStartGameplay = true;
            PlayerController.main.RestartPosition();
    }

    public void StartGameplay()
    {
        numDeath = 0;
        isStartGameplay = true;
        Time.timeScale = 1;
        startTime = Time.time;
    }

    public void StopGameplay()
    {
        numDeath++;
        isStartGameplay = false;
        Time.timeScale = 0;
        UIController.main.StopGame();
        MainStats.main.record.Save((int)(Time.time - startTime));
    }

    public void ChangeSpeed()
    {
        Time.timeScale = Time.timeScale == _sceneSlowlySpeed ? 1 : _sceneSlowlySpeed;
    }
}
