using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController main;

    [SerializeField] private GameObject _StartUI;
    [SerializeField] private GameObject _GameplayUI;
    [SerializeField] private GameObject _EndUI;

    [SerializeField] private Text _RecordText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _ContinueGameText;
    [SerializeField] private List<Text> _moneyText = new List<Text>();

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        RestartGame();
    }

    public void SetScoreText(int value)
    {
        _scoreText.text = $"{value}";
    }

    public void UpdateMoneyText()
    {
        foreach(Text text in _moneyText)
        {
            text.text = $"Money:{MainStats.main.money.value}";
        }
    }

    public void StartGame()
    {
        GameplaySceneController.main.StartGameplay();
        _StartUI.SetActive(false);
        _GameplayUI.SetActive(true);
    }
    public void StopGame()
    {
        _GameplayUI.SetActive(false);
        _EndUI.SetActive(true);
        _ContinueGameText.text = $"Press to continue game {GameplaySceneController.main.numDeath * GameplaySceneController.main.costContinueGame} money";
    }

    public void RestartGame()
    {
        _RecordText.text = $"Best result:\n{MainStats.main.record.value}";
        _StartUI.SetActive(true);
        _EndUI.SetActive(false);
        _GameplayUI.SetActive(false);
        GameplaySceneController.main.RestartLevel();
    }

    public void ContinueGame()
    {
        if (MainStats.main.money.Minus(GameplaySceneController.main.numDeath * GameplaySceneController.main.costContinueGame))
        {
            _StartUI.SetActive(false);
            _EndUI.SetActive(false);
            _GameplayUI.SetActive(true);
            GameplaySceneController.main.CountineLevel();
            UIController.main.UpdateMoneyText();
        }
    }
}
