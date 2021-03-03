using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public Animator pow;
    public float hitForce = 5;
    public ParticleSystem[] particleSystems;

    //mobile
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    public CheckZone checkZone;

    // Start is called before the first frame update

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        SetActivePaw();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckZone.isDead)
        {
            pow.SetBool("Left", false);
            pow.SetBool("Right", false);
            return;
        }


        //Computer testing
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pow.SetBool("Left", true);
            HitObjectLeft();
            HitZone.canHit = false;
            goto END;
        }
        else
        {
            pow.SetBool("Left", false);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            pow.SetBool("Right", true);
            HitObjectRight();
            HitZone.canHit = false;
            goto END;
        }
        else
        {
            pow.SetBool("Right", false);
        }

        //mobile input

        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            pow.SetBool("Right", true);
                            HitObjectRight();
                            HitZone.canHit = false;
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            pow.SetBool("Left", true);
                            HitObjectLeft();
                            HitZone.canHit = false;
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
        else
        {
            pow.SetBool("Left", false);
            pow.SetBool("Right", false);
        }

    END:;
    }

    public void HitObjectLeft()
    {
        //If in hit zone and there is something
        if (HitZone.canHit && HitZone.currentObject != null)
        {
            if (HitZone.currentObject.name == "BonusCat")
            {
                checkZone.GoodEat();
            }
            
            //if hit in wrong direction or it is good
            else if (Spawner.direction != "Left" || HitZone.currentObject.CompareTag("Good"))
            {
                checkZone.OnDeath();
            }
            else
            {
                checkZone.GoodEat();
            }
            HitZone.currentObject.GetComponent<ObjectMovement>().enabled = false;

            Rigidbody rb = HitZone.currentObject.GetComponent<Rigidbody>();

            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(-Random.Range(0.5f, 1), Random.Range(0.5f, 1), 0) * hitForce, ForceMode.Impulse);
            rb.AddTorque(new Vector3(0, 0, 1) * Random.Range(50,250));
            rb.useGravity = true;
        }
    }

    public void HitObjectRight()
    {
        if (HitZone.canHit && HitZone.currentObject != null)
        {
            if (HitZone.currentObject.name == "BonusCat")
            {
                checkZone.GoodEat();
            }
            
            //if hit in wrong direction or it is good
            else if (Spawner.direction != "Right" || HitZone.currentObject.CompareTag("Good"))
            {
                checkZone.OnDeath();
            }
            else
            {
                checkZone.GoodEat();
            }
            HitZone.currentObject.GetComponent<ObjectMovement>().enabled = false;

            Rigidbody rb = HitZone.currentObject.GetComponent<Rigidbody>();

            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(Random.Range(0.5f, 1), Random.Range(0.5f, 1), 0) * hitForce, ForceMode.Impulse);
            rb.AddTorque(new Vector3(0, 0, -1) * Random.Range(50, 250));
            rb.useGravity = true;
        }
    }

    public void SetActivePaw()
    {
        int ID = PlayerPrefs.GetInt("ActivePaw");
        foreach (Transform paw in transform.GetChild(0))
        {
            paw.gameObject.SetActive(false);
        }
        
        transform.GetChild(0).GetChild(ID).gameObject.SetActive(true);
        
        //setting particle systems
        if (ID == 1)
        {
            particleSystems[0].Play();
        }
        else
        {
            particleSystems[0].Stop();
        }
    }
}
