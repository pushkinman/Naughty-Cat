using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckZone : MonoBehaviour
{
    public static bool isDead;
    public GameObject canvasGameOver;
    public static int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI coinsText;

    public Animator cameraAnimator;

    public Color colorGood;
    public Color colorBad;
    public Animator imageGradientAnimator;
    public float respawnTime = 2;

    public AudioSource successSound;
    public AudioSource gameOverSound;
    public AudioSource coinsSound;

    public GameObject vibration;
    public ParticleSystem coins;

    private void Start()
    {
        canvasGameOver.SetActive(false);
        scoreText.text = score.ToString();
        isDead = false;
        score = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnRespawn();
        }
    }

    public void OnLevelWasLoaded()
    {
        isDead = false;
        score = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bad"))
        {
            OnDeath();
        }
        else if (other.CompareTag("Good"))
        {
            GoodEat();
            CheckForBonus(other.name);
        }
    }

    private static float lastSpeed;
    public void OnDeath()
    {
        isDead = true;

        PlayGradient(colorBad);

        if (cameraAnimator != null)
            cameraAnimator.SetTrigger("Death");

        UIManager.GameOverCanvas.SetActive(true);
        UIManager.PauseButton.SetActive(false);

        //Saving data

        if (score > SaveData.GetHighscore())
        {
            SaveData.SetNewHighscore(score);
        }
        PlayerPrefs.SetInt("High", score);

        highscoreText.text = "Highscore: " + SaveData.GetHighscore();

        SaveData.AddCoins(score / 10);
        coinsText.text = SaveData.GetCoins().ToString();
        coinsText.GetComponent<Animator>().SetTrigger("Pop");

        lastSpeed = ObjectMovement.speed;
        ObjectMovement.speed = 0;
        if (HitZone.currentObject != null)
        {
            HitZone.currentObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            HitZone.currentObject.GetComponent<Rigidbody>().useGravity = true;
        }
        
        gameOverSound.Play();
        vibration.GetComponent<Vibrate>().SetVibrate();
    }

    public void OnRespawn()
    {
        UIManager.GameOverCanvas.SetActive(false);
        UIManager.PauseButton.SetActive(true);
        UIManager.InGameCanvas.SetActive(true);
        UIManager.ImageLoad.SetActive(true);
        UIManager.ImageLoad.GetComponent<Image>().fillAmount = 0;
        loadingImage = UIManager.ImageLoad.GetComponent<Image>();
        StartCoroutine(LoadRespawn());
        StartCoroutine(LoadImageFilling());
    }

    public void GoodEat()
    {
        if (HitZone.currentObject.name == "BonusCat")
            return;
        
        score += 10;
        scoreText.text = score.ToString();
        Animator scoreAnimator = scoreText.GetComponent<Animator>();
        if (scoreAnimator != null)
            scoreAnimator.SetTrigger("Pop");
        PlayGradient(colorGood);
        Parameters.SpeedIncrease();
       
        //save data
        int currectnumber = PlayerPrefs.GetInt(HitZone.currentObject.name);
        currectnumber++;
        PlayerPrefs.SetInt(HitZone.currentObject.name, currectnumber);
        print(HitZone.currentObject.name + " " + currectnumber);
        
        successSound.Play();
    }

    public void PlayGradient(Color color)
    {
        foreach (Transform child in imageGradientAnimator.gameObject.transform)
            child.GetComponent<Image>().color = color;

        imageGradientAnimator.SetTrigger("Pop");
    }

    IEnumerator LoadRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        UIManager.ImageLoad.SetActive(false);
        isDead = false;
        ObjectMovement.speed = lastSpeed;
    }

    private Image loadingImage;
    IEnumerator LoadImageFilling()
    {
        while (loadingImage.fillAmount < 1)
        {
            loadingImage.fillAmount += Time.deltaTime / respawnTime;
            yield return null;
        }
    }
    
    public void CheckForBonus(string name)
    {
        if (name == "BonusCat")
        {
            SaveData.AddCoins(100);
            coinsText.text = SaveData.GetCoins().ToString();
            coinsText.GetComponent<Animator>().SetTrigger("Pop");
            
            coins.Play();
            coinsSound.Play();
        }
    }
}
