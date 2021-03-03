using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject InGameCanvasObject;
    public static GameObject InGameCanvas;
    public bool InGameCanvasSetActive;

    public GameObject PauseMenuCanvasObject;
    public static GameObject PauseMenuCanvas;
    public bool PauseMenuCanvasSetActive;

    public GameObject GameOverCanvasObject;
    public static GameObject GameOverCanvas;
    public bool GameOverCanvasSetActive;

    public GameObject PauseButtonObject;
    public static GameObject PauseButton;
    public bool PauseButtonSetActive;
    
    public GameObject ImageLoadObject;
    public static GameObject ImageLoad;
    public bool ImageLoadSetActive;
    
    public GameObject ButtonAdRewardCoinsObject;
    public static GameObject ButtonAdRewardCoins;
    public bool ButtonAdRewardCoinsSetActive;
    
    public GameObject ButtonAdRewardRespawnObject;
    public static GameObject ButtonAdRewardRespawn;
    public bool ButtonAdRewardRespawnSetActive;

    // Start is called before the first frame update
    void Start()
    {
        InGameCanvas = InGameCanvasObject;
        InGameCanvas.SetActive(InGameCanvasSetActive);

        PauseMenuCanvas = PauseMenuCanvasObject;
        PauseMenuCanvas.SetActive(PauseMenuCanvasSetActive);

        GameOverCanvas = GameOverCanvasObject;
        GameOverCanvas.SetActive(GameOverCanvasSetActive);

        PauseButton = PauseButtonObject;
        PauseButton.SetActive(PauseButtonSetActive);

        ImageLoad = ImageLoadObject;
        ImageLoad.SetActive(ImageLoadSetActive);
        
        ButtonAdRewardCoins = ButtonAdRewardCoinsObject;
        ButtonAdRewardCoins.SetActive(ButtonAdRewardCoinsSetActive);
        
        ButtonAdRewardRespawn = ButtonAdRewardRespawnObject;
        ButtonAdRewardRespawn.SetActive(ButtonAdRewardRespawnSetActive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
