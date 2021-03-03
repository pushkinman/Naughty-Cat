using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnPoint;
    public static float timePeriod = 2f;
    public float timePeriodValue = 2f;
    private float actualTime;

    public GameObject[] objectGoodPrefabs;
    public GameObject[] objectBadPrefabs;
    public GameObject objectCatBonus;
    public int objectCatBonusRandomValue = 50;

    public GameObject rightArrow;
    public GameObject leftArrow;
    public static string direction = "";
    
    private int inARowLimit = 3;

    public bool testing = false;
    public bool isGood = true;
    public int obj;

    public bool tutorial = false;

    // Start is called before the first frame update
    void Start()
    {
        timePeriod = timePeriodValue;
        actualTime = timePeriod;
        
        //arrows
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckZone.isDead)
        {
            rightArrow.SetActive(false);
            leftArrow.SetActive(false);
            return;
        }

        actualTime -= Time.deltaTime;

        if (actualTime < 0)
        {
            SpawnObject();
            RandomArrow();
            actualTime = timePeriod;
        }
    }

    int maxGood = 0;
    int maxBad = 0;
    
    public void SpawnObject()
    {
        int random = Random.Range(0, 2);
        int randomGood = Random.Range(0, objectGoodPrefabs.Length);
        int randomBad = Random.Range(0, objectBadPrefabs.Length);
        
        int randomBonus = Random.Range(0, objectCatBonusRandomValue);
        int randomBonusValue = 7;
        bool spawnBonus = randomBonus == randomBonusValue;

        //check for max in a row

        if (random == 0)
        {
            maxGood++;
            if (maxGood >= inARowLimit)
            {
                random = 1;
                maxGood = 0;
            }
        }
        
        if (random == 1)
        {
            maxBad++;
            if (maxBad >= inARowLimit)
            {
                random = 1;
                maxBad = 0;
            }
        }

        if (testing)
        {
            if (isGood)
            {
                GameObject prefab = objectGoodPrefabs[this.obj];
                GameObject obj = Instantiate(prefab, spawnPoint.position, prefab.transform.rotation);
                obj.name = prefab.name;
                Destroy(obj, 10);
            }
            else
            {
                GameObject prefab = objectBadPrefabs[this.obj];
                GameObject obj = Instantiate(prefab, spawnPoint.position, prefab.transform.rotation);
                obj.name = prefab.name;
                Destroy(obj, 10);
            }
            return;
        }

        if (spawnBonus && !tutorial)
        {
            GameObject prefab = objectCatBonus;
            GameObject obj = Instantiate(prefab, spawnPoint.position, prefab.transform.rotation);
            obj.name = prefab.name;
            Destroy(obj, 10);
            return;
        }
        
        //spawn
        if (random == 0)
        {
            GameObject prefab = objectGoodPrefabs[randomGood];
            GameObject obj = Instantiate(prefab, spawnPoint.position, prefab.transform.rotation);
            obj.name = prefab.name;
            Destroy(obj, 10);
        }
        else
        {
            GameObject prefab = objectBadPrefabs[randomBad];
            GameObject obj = Instantiate(prefab, spawnPoint.position, prefab.transform.rotation);
            obj.name = prefab.name;
            Destroy(obj, 10);
        }
    }

    int right = 0;
    int left = 0;

    public void RandomArrow()
    {
        int rand = Random.Range(0, 2);

        if (rand == 0)
        {
            right++;
            if (right >= inARowLimit)
            {
                rand = 1;
                right = 0;
            }
        }
        
        if (rand == 1)
        {
            left++;
            if (left >= inARowLimit)
            {
                rand = 1;
                left = 0;
            }
        }
        
        if(rand == 0)
        {
            rightArrow.SetActive(true);
            leftArrow.SetActive(false);
            direction = "Right";
        }
        else
        {
            rightArrow.SetActive(false);
            leftArrow.SetActive(true);
            direction = "Left";
        }

    }
}
