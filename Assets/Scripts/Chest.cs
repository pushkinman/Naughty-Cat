using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public float msToWait = 5000f; //1.44e+07
    public TextMeshProUGUI chestTimer;
    public Button chestButton;
    public ParticleSystem coinParticleSystem;
    public TextMeshProUGUI coins;
    private ulong lastChestOpen;

    public NotificationsManager notificationsManager;

    private void Start()
    {
        lastChestOpen = ulong.Parse(PlayerPrefs.GetString("LastChestOpen"));
        if (!IsChestReady())
        {
            chestButton.interactable = false;
            chestButton.GetComponent<Animator>().enabled = false;
        }
        else
        {
            chestButton.GetComponent<Animator>().enabled = true;
        }
            
        chestTimer.text = "Ready!";
    }

    private void Update()
    {
        if (!chestButton.IsInteractable())
        {
            if (IsChestReady())
            {
                chestButton.interactable = true;
                chestButton.GetComponent<Animator>().enabled = true;
                return;
            }

            // Set timer

            ulong diff = ((ulong)DateTime.Now.Ticks - lastChestOpen);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float secondsLeft = (msToWait - m) / 1000f;

            string r = "";
            // Hours
            r += ((int)secondsLeft / 3600).ToString() + "h ";
            secondsLeft -= ((int)secondsLeft / 3600) * 3600;
            // Minutes
            r += ((int)secondsLeft / 60).ToString("00") + "m ";
            // Seconds
            r += (secondsLeft % 60).ToString("00") + "s";
            chestTimer.text = r;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            lastChestOpen = 99999999999999;
        }
    }

    public void ChestClick()
    {
        lastChestOpen = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("LastChestOpen", DateTime.Now.Ticks.ToString());
        chestButton.interactable = false;
        chestButton.GetComponent<Animator>().Rebind();
        chestButton.GetComponent<Animator>().enabled = false;
        coinParticleSystem.Play();
        SaveData.AddCoins(500);
        coins.text = SaveData.GetCoins().ToString();
        coins.GetComponent<Animator>().SetTrigger("Pop");
        notificationsManager.CreateNotification(msToWait);
    }

    private bool IsChestReady()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastChestOpen);
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float secondsLeft = (msToWait - m) / 1000f;

        if (secondsLeft < 0)
        {
            chestTimer.text = "Ready!";
            return true;
        }

        return false;
    }
}
