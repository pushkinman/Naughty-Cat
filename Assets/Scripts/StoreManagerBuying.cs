using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManagerBuying : MonoBehaviour
{
    public static int activePaw;

    public GameObject[] powSkinsButtons;
    public TextMeshProUGUI coins;
    public TextMeshProUGUI coinsMain;
    public ParticleSystem konfetti;
    public AudioSource soundSelect;
    public AudioSource soundUnlock;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePowSkins();
        SetActivePaw(PlayerPrefs.GetInt("ActivePaw"));
        coins.text = SaveData.GetCoins().ToString();
        coins.GetComponent<Animator>().SetTrigger("Pop");
        PlayerPrefs.SetInt("ID" + 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ClearPowSkins();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Addcoins(10000);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RemoveAllCoins();
        }

        //Debug.Log("Active paw " + activePaw);
    }

    public void BuyItem(GameObject button)
    {
        int ID = button.GetComponent<Info>().ID;

        if(PlayerPrefs.GetInt("ID" + ID) == 1)
        {
            SetActivePaw(ID);
            soundSelect.Play();
            return;
        }

        int price = button.GetComponent<Info>().price;

        if (price <= SaveData.GetCoins())
        {
            SaveData.RemoveCoins(price);
            PlayerPrefs.SetInt("ID" + ID, 1);
            SetActivePaw(ID);
            UpdatePowSkins();
            coins.text = SaveData.GetCoins().ToString();
            coinsMain.text = SaveData.GetCoins().ToString();
            coins.GetComponent<Animator>().SetTrigger("Pop");
            konfetti.Play();
            soundUnlock.Play();
        }
    }

    public void UpdatePowSkins()
    {
        foreach (GameObject powSkinButton in powSkinsButtons)
        {
            int ID = powSkinButton.GetComponent<Info>().ID;

            if(ID == 0)
            {
                powSkinButton.transform.GetChild(2).gameObject.SetActive(false);
                powSkinButton.transform.GetChild(3).gameObject.SetActive(false);
                continue;
            }

            if (PlayerPrefs.GetInt("ID" + ID) == 1)
            {
                powSkinButton.transform.GetChild(2).gameObject.SetActive(false);
                powSkinButton.transform.GetChild(3).gameObject.SetActive(false);
            }
            else
            {
                powSkinButton.transform.GetChild(2).gameObject.SetActive(true);
                powSkinButton.transform.GetChild(3).gameObject.SetActive(true);
            }
        }
    }

    public void ClearPowSkins()
    {
        foreach (GameObject powSkinButton in powSkinsButtons)
        {
            int ID = powSkinButton.GetComponent<Info>().ID;

            if (ID == 0)
            {
                powSkinButton.transform.GetChild(2).gameObject.SetActive(false);
                powSkinButton.transform.GetChild(3).gameObject.SetActive(false);
                continue;
            }

            PlayerPrefs.SetInt("ID" + ID, 0);
        }
        UpdatePowSkins();
    }

    public void Addcoins(int coin)
    {
        SaveData.AddCoins(coin);
        coins.text = SaveData.GetCoins().ToString();
        coinsMain.text = SaveData.GetCoins().ToString();
        coins.GetComponent<Animator>().SetTrigger("Pop");
    }

    public void RemoveAllCoins()
    {
        SaveData.SetCoinsTo(0);
        coins.text = SaveData.GetCoins().ToString();
        coinsMain.text = SaveData.GetCoins().ToString();
        coins.GetComponent<Animator>().SetTrigger("Pop");
    }

    public void SetActivePaw(int ID)
    {
        activePaw = ID;
        PlayerPrefs.SetInt("ActivePaw", activePaw);
        foreach (GameObject powSkinButton in powSkinsButtons)
        {
            if (powSkinButton.GetComponent<Info>().ID != activePaw)
                powSkinButton.transform.GetChild(0).gameObject.SetActive(false);
            else
                powSkinButton.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
