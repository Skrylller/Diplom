using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStats : MonoBehaviour
{
    public static MainStats main;

    public class Stat
    {
        private bool _isBestValue;
        public int value { get; private set; }
        private string _key;

        public Stat(bool isBestValue, string key, List<Stat> stats)
        {
            _isBestValue = isBestValue;
            _key = key;
            stats.Add(this);
        }

        public void Add(int value)
        {
            if (_isBestValue)
                return;

            this.value += value;
            PlayerPrefs.SetInt(_key, this.value);
        }

        public bool Minus(int value)
        {
            if (_isBestValue || this.value - value < 0)
                return false;

            this.value -= value;
            PlayerPrefs.SetInt(_key, this.value);
            return true;
        }

        public void Save(int value)
        {
            if (!_isBestValue || PlayerPrefs.GetInt(_key, 0) < value)
            {
                this.value = value;
                PlayerPrefs.SetInt(_key, this.value);
            }
        }

        public void Load()
        {
            value = PlayerPrefs.GetInt(_key, 0);
        }
    }

    private static List<Stat> stats = new List<Stat>();

    public Stat money = new Stat(false, "money", stats);
    public Stat record = new Stat(true, "record", stats);


    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        LoadStats();
    }

    private void LoadStats()
    {
        foreach(Stat stat in stats)
        {
            stat.Load();
        }

        UIController.main.UpdateMoneyText();
    }
}
