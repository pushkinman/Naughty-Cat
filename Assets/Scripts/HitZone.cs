using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    public static bool canHit;
    public static GameObject currentObject;

    private void OnTriggerEnter(Collider other)
    {
        canHit = true;
        currentObject = other.gameObject;
        //ParticleSystem particleSystem = other.GetComponent<ParticleSystem>();
        //if (particleSystem != null)
        //{
        //    particleSystem.Play();
        //    Debug.Log("plaing particle system");
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        canHit = false;
        //currentObject = null;
    }
}
