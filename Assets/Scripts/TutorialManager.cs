using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject buttonStartGame;
    public GameObject buttonStartTutorial;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SetTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerPrefs.SetInt("Tutorial", 0);
            SetTutorial();
        }
    }

    public void SetTutorial()
    {
        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            buttonStartGame.SetActive(false);
            buttonStartTutorial.SetActive(true);
        }
        else if(PlayerPrefs.GetInt("Tutorial") == 1)
        {
            buttonStartGame.SetActive(true);
            buttonStartTutorial.SetActive(false);
        }
    }
}
