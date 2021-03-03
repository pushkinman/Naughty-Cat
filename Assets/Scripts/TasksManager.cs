using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TasksManager : MonoBehaviour
{
    public GameObject buttonTask1;
    public GameObject buttonTask2;
    public GameObject buttonTask3;
    public int tasksShown = 3;
    private int actualTaskShown;
    private List<TaskInfo> activeTasks = new List<TaskInfo>();
    
    public ParticleSystem coinParticleSystem;

    public TextMeshProUGUI coinsMain;
    public TextMeshProUGUI coinsStore;

    public Animator animatorDailyTasks;

    
    // Start is called before the first frame update
    void Start()
    {
        UpdateTasks();
        CheckDailyTasksStatus();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerPrefs.SetInt("task" + 0, 0);
            PlayerPrefs.SetInt("Apple", 0);
            PlayerPrefs.SetInt("task" + 1, 0);
            PlayerPrefs.SetInt("task" + 2, 0);
            PlayerPrefs.SetInt("task" + 3, 0);
            PlayerPrefs.SetInt("task" + 4, 0);
            SaveData.SetNewHighscore(0);
            UpdateTasks();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerPrefs.SetInt("Apple", 20);
            PlayerPrefs.SetInt("High", 200);
            UpdateTasks();
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            foreach (TaskInfo task in activeTasks)
            {
                Debug.Log(task.taskNumber + " " +
                          task.completed + " " +
                          task.count + " " +
                          task.type + " " +
                          task.itemName + " " +
                          task.reward);
            }
        }
    }
    
    public void UpdateTasks()
    {
        TasksData.UpdateAllTasks();
        activeTasks.Clear();
        actualTaskShown = 0;
        
        foreach (TaskInfo task in TasksData.tasks)
        {
            if (task.completed == 1)
                continue;
            
            if(actualTaskShown >= tasksShown)
                break;

            activeTasks.Add(task);
            actualTaskShown++;
        }
        
        //button1
        if (activeTasks[0].type == "EatFood")
        {
            CollectFood(buttonTask1, activeTasks[0]);
        }
        else if (activeTasks[0].type == "Throw")
        {
            HitNotFood(buttonTask1, activeTasks[0]);
        }
        else if (activeTasks[0].type == "Points")
        {
            GetPoints(buttonTask1, activeTasks[0]);
        }
        else if (activeTasks[0].type == "Ad")
        {
            WatchAd(buttonTask1, activeTasks[0]);
        }
        //button1
        if (activeTasks[1].type == "EatFood")
        {
            CollectFood(buttonTask2, activeTasks[1]);
        }
        else if (activeTasks[1].type == "Throw")
        {
            HitNotFood(buttonTask2, activeTasks[1]);
        }
        else if (activeTasks[1].type == "Points")
        {
            GetPoints(buttonTask2, activeTasks[1]);
        }
        else if (activeTasks[1].type == "Ad")
        {
            WatchAd(buttonTask2, activeTasks[1]);
        }
        //button1
        if (activeTasks[2].type == "EatFood")
        {
            CollectFood(buttonTask3, activeTasks[2]);
        }
        else if (activeTasks[2].type == "Throw")
        {
            HitNotFood(buttonTask3, activeTasks[2]);
        }
        else if (activeTasks[2].type == "Points")
        {
            GetPoints(buttonTask3, activeTasks[2]);
        }
        else if (activeTasks[2].type == "Ad")
        {
            WatchAd(buttonTask3, activeTasks[2]);
        }
    }

    public void CollectFood(GameObject button, TaskInfo task)
    {
        int currectNumberOfTimes = PlayerPrefs.GetInt(task.itemName);

        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Collect " + task.count + " " + task.itemName + "s";
        button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currectNumberOfTimes + "/" + task.count;
        button.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = task.reward.ToString();
        if (currectNumberOfTimes >= task.count)
        {
            button.GetComponent<Button>().interactable = true;
            button.GetComponent<Animator>().SetBool("Complete", true);
        }
        else
        {
            button.GetComponent<Button>().interactable = false;
            button.GetComponent<Animator>().SetBool("Complete", false);
        }
    }
    
    public void HitNotFood(GameObject button, TaskInfo task)
    {
        int currectNumberOfTimes = PlayerPrefs.GetInt(task.itemName);

        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Throw " + task.itemName + " " + task.count + " times";
        button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currectNumberOfTimes + "/" + task.count;
        button.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = task.reward.ToString();
        if (currectNumberOfTimes >= task.count)
        {
            button.GetComponent<Button>().interactable = true;
            button.GetComponent<Animator>().SetBool("Complete", true);
        }
        else
        {
            button.GetComponent<Button>().interactable = false;
            button.GetComponent<Animator>().SetBool("Complete", false);
        }
    }
    
    public void GetPoints(GameObject button, TaskInfo task)
    {
        int currectNumberOfTimes = PlayerPrefs.GetInt("High");

        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Get Score of " + task.count;
        button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currectNumberOfTimes + "/" + task.count;
        button.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = task.reward.ToString();
        if (currectNumberOfTimes >= task.count)
        {
            button.GetComponent<Button>().interactable = true;
            button.GetComponent<Animator>().SetBool("Complete", true);
        }
        else
        {
            button.GetComponent<Button>().interactable = false;
            button.GetComponent<Animator>().SetBool("Complete", false);
        }
    }
    
    public void WatchAd(GameObject button, TaskInfo task)
    {
        int currectNumberOfTimes = PlayerPrefs.GetInt("Ad");

        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Watch an Ad " + task.count + " times";
        button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currectNumberOfTimes + "/" + task.count;
        button.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = task.reward.ToString();
        if (currectNumberOfTimes >= task.count)
        {
            button.GetComponent<Button>().interactable = true;
            button.GetComponent<Animator>().SetBool("Complete", true);
        }
        else
        {
            button.GetComponent<Button>().interactable = false;
            button.GetComponent<Animator>().SetBool("Complete", false);
        }
    }

    public void TakeTaskReward(GameObject button)
    {
        if (button.name == "ButtonTask1")
        {
            activeTasks[0].completed = 1;
            PlayerPrefs.SetInt("task" + TasksData.tasks[activeTasks[0].taskNumber].taskNumber, 1);
            ClearAllNonActiveItemValues();
            AddCoins(activeTasks[0].reward);
            UpdateTasks();
        }
        else if (button.name == "ButtonTask2")
        {
            activeTasks[1].completed = 1;
            PlayerPrefs.SetInt("task" + TasksData.tasks[activeTasks[1].taskNumber].taskNumber, 1);
            ClearAllNonActiveItemValues();
            AddCoins(activeTasks[1].reward);
            UpdateTasks();
        }
        else if (button.name == "ButtonTask3")
        {
            activeTasks[2].completed = 1;
            PlayerPrefs.SetInt("task" + TasksData.tasks[activeTasks[2].taskNumber].taskNumber, 1);
            ClearAllNonActiveItemValues();
            AddCoins(activeTasks[2].reward);
            UpdateTasks();
        }

        CheckDailyTasksStatus();
    }

    public void AddCoins(int coins)
    {
        SaveData.AddCoins(coins);
        coinParticleSystem.Play();

        coinsMain.text = SaveData.GetCoins().ToString();
        coinsStore.text = SaveData.GetCoins().ToString();
    }

    public void ClearAllNonActiveItemValues()
    {
        foreach (string itemName in TasksData.itemNames)
        {
            if (itemName != activeTasks[0].itemName &&
                itemName != activeTasks[1].itemName &&
                itemName != activeTasks[2].itemName)
            {
                PlayerPrefs.SetInt(itemName, 0);
            }
        }
    }

    public void CheckDailyTasksStatus()
    {
        if (buttonTask1.GetComponent<Button>().IsInteractable() ||
            buttonTask2.GetComponent<Button>().IsInteractable() ||
            buttonTask3.GetComponent<Button>().IsInteractable())
        {
            animatorDailyTasks.SetBool("Completed", true);
        }
        else
        {
            animatorDailyTasks.SetBool("Completed", false);
        }
    }
}
