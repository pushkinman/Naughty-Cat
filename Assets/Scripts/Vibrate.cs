using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrate : MonoBehaviour
{
    public static bool canVibrate = true;
    private void Start()
    {
        Vibration.Init();
    }

    public void SetVibrate()
    {
        if(!canVibrate)
            return;
        
        Vibration.VibratePop();
        Debug.Log("Vibrating");
    }

    IEnumerator Vib()
    {
        Handheld.Vibrate();
        yield return new WaitForSeconds(100);
        Handheld.Vibrate();
    }
}
