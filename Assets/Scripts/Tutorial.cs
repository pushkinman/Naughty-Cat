using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject textGood;
    public GameObject textBadRight;
    public GameObject textBadLeft;
    public GameObject buttonTryAgain;
    public GameObject buttonComplete;
    public ParticleSystem particleSystem;

    public float slowTime = 0.4f;
    private bool once = true;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        textGood.SetActive(false);
        textBadRight.SetActive(false);
        textBadLeft.SetActive(false);
        buttonTryAgain.SetActive(false);
        buttonComplete.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (HitZone.currentObject == null)
        {
            ObjectMovement.speed = 2f;
            Spawner.timePeriod = 4f;
            return;
        }
        
        
        if (HitZone.currentObject != null)
        {
            ObjectMovement.speed = 2f;
            Spawner.timePeriod = 4f;
        }

        if (HitZone.currentObject.CompareTag("Good"))
        {
            textGood.SetActive(true);
            textBadRight.SetActive(false);
            textBadLeft.SetActive(false);
        }
        if (HitZone.currentObject.CompareTag("Bad"))
        {
            textGood.SetActive(false);
            if (Spawner.direction == "Right")
            {
                textBadRight.SetActive(true);
                textBadLeft.SetActive(false);
            }
            else if (Spawner.direction == "Left")
            {
                textBadLeft.SetActive(true);
                textBadRight.SetActive(false);
            }
        }
        
        if (CheckZone.isDead)
        {
            UIManager.GameOverCanvas.SetActive(false);
            textGood.SetActive(false);
            textBadRight.SetActive(false);
            textBadLeft.SetActive(false);
            buttonTryAgain.SetActive(true);
        }

        if (CheckZone.score >= 50)
        {
            textGood.SetActive(false);
            textBadRight.SetActive(false);
            textBadLeft.SetActive(false);
            Time.timeScale = 1f;
            ObjectMovement.speed = 4f;
            Spawner.timePeriod = 3f;
        }
        
        if (CheckZone.score >= 100)
        {
            CheckZone.isDead = true;
            textGood.SetActive(false);
            textBadRight.SetActive(false);
            textBadLeft.SetActive(false);
            buttonTryAgain.SetActive(false);
            buttonComplete.SetActive(true);
            //save data
            particleSystem.Play();
            Debug.Log("particle");
            if (once)
            {
                PlayerPrefs.SetInt("Tutorial", 1);
                once = false;
            }
        }
        
    }
}
