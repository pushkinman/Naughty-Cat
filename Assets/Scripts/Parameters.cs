using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour
{
    public float speed = 4;

    // Start is called before the first frame update
    void Start()
    {
        ObjectMovement.speed = speed;

    }

    public void OnLevelWasLoaded()
    {
        ObjectMovement.speed = speed;
    }

    private void LateUpdate()
    {
        print(ObjectMovement.speed);
    }

    public static void SpeedIncrease()
    {
        if (CheckZone.score == 50)
        {
            ObjectMovement.speed *= 1.2f;
        }
        if (CheckZone.score == 100)
        {
            ObjectMovement.speed *= 1.2f;
        }
        if (CheckZone.score == 150)
        {
            ObjectMovement.speed *= 1.2f;
        }
        if (CheckZone.score == 200)
        {
            ObjectMovement.speed *= 1.2f;
        }
        if (CheckZone.score == 250)
        {
            ObjectMovement.speed *= 1.2f;
        }
        if (CheckZone.score == 300)
        {
            ObjectMovement.speed *= 1.2f;
        }
    }
}
