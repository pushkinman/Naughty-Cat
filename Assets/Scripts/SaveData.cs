using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public TextMeshProUGUI highscore;
    public TextMeshProUGUI coins;
    public TextMeshProUGUI coins2;

    public void Start()
    {
        highscore.text = "Highscore " + GetHighscore().ToString();
        coins.text = GetCoins().ToString();
    }

    public static void SetNewHighscore(int score)
    {
        PlayerPrefs.SetInt("HighScore", score);
    }

    public static int GetHighscore()
    {
        return PlayerPrefs.GetInt("HighScore");
    }

    public static void AddCoins(int coins)
    {
        int coinsMemory = PlayerPrefs.GetInt("Coins");
        coinsMemory += coins;
        PlayerPrefs.SetInt("Coins", coinsMemory);
    }

    public static void RemoveCoins(int coins)
    {
        int coinsMemory = PlayerPrefs.GetInt("Coins");
        coinsMemory -= coins;
        PlayerPrefs.SetInt("Coins", coinsMemory);
    }

    public static int GetCoins()
    {
        return PlayerPrefs.GetInt("Coins");
    }

    public static void SetCoinsTo(int coins)
    {
        PlayerPrefs.SetInt("Coins", coins);

    }

    public void UpdateCoinsUI()
    {
        coins.text = GetCoins().ToString();
        coins2.text = GetCoins().ToString();
    }
}
