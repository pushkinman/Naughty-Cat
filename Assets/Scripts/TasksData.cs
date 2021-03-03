using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class TasksData : MonoBehaviour
{
    public static List<TaskInfo> tasks = new List<TaskInfo>();
    public static HashSet<string> itemNames = new HashSet<string>();
    public static int numberOfTasks = 37;

    private void Awake()
    {
        UpdateAllTasks();
        print(PlayerPrefs.GetInt("task0"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            foreach (TaskInfo task in tasks)
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

    public static void UpdateAllTasks()
    {
        //creating tasks
        for (int i = 0; i < numberOfTasks; i++)
        {
            tasks.Add(new TaskInfo());
        }
        //getting their status of completion
        for (int i = 0; i < numberOfTasks; i++)
        {
            int completed = PlayerPrefs.GetInt("task" + i);
            tasks[i].completed = completed;
        }
        //adding tasks them selves
        AddTaskInfo(0, "EatFood", "Apple", 8, 50);
        AddTaskInfo(1, "Throw", "Pan", 10, 50);
        AddTaskInfo(2, "Points", "High", 200, 100);
        AddTaskInfo(3, "Ad", "Ad", 1, 100);
        AddTaskInfo(4, "EatFood", "Donut", 12, 100);
        AddTaskInfo(5, "Throw", "Ball", 15, 100);
        AddTaskInfo(6, "Points", "High", 250, 150);
        AddTaskInfo(7, "EatFood", "Pear", 20, 150);
        AddTaskInfo(8, "Throw", "Alarm", 25, 150);
        AddTaskInfo(9, "Ad", "Ad", 2, 200);
        AddTaskInfo(10, "EatFood", "Fries", 32, 200);
        AddTaskInfo(11, "Throw", "Tv", 40, 200);
        AddTaskInfo(12, "Points", "High", 300, 200);
        AddTaskInfo(13, "EatFood", "Sandwich", 40, 250);
        AddTaskInfo(14, "Throw", "Fan", 45, 250);
        AddTaskInfo(15, "Ad", "Ad", 3, 300);
        AddTaskInfo(16, "EatFood", "Orange", 40, 300);
        AddTaskInfo(17, "Throw", "Microwave", 45, 300);
        AddTaskInfo(18, "Points", "High", 350, 300);
        AddTaskInfo(19, "EatFood", "Pepper", 40, 350);
        AddTaskInfo(20, "Throw", "Mixer", 45, 350);
        AddTaskInfo(21, "Ad", "Ad", 4, 400);
        AddTaskInfo(22, "EatFood", "Tomato", 40, 400);
        AddTaskInfo(23, "Throw", "Barrel", 45, 400);
        AddTaskInfo(24, "Points", "High", 400, 400);
        AddTaskInfo(25, "EatFood", "Watermelon", 40, 450);
        AddTaskInfo(26, "Throw", "Hat", 45, 450);
        AddTaskInfo(27, "Ad", "Ad", 5, 500);
        AddTaskInfo(28, "EatFood", "Cheeseburger", 40, 500);
        AddTaskInfo(29, "Throw", "Candelabra", 45, 500);
        AddTaskInfo(30, "Points", "High", 450, 500);
        AddTaskInfo(31, "EatFood", "Hotdog", 40, 550);
        AddTaskInfo(32, "Throw", "Rubik's cube", 45, 550);
        AddTaskInfo(33, "Ad", "Ad", 10, 500);
        AddTaskInfo(34, "EatFood", "Taco", 100, 1000);
        AddTaskInfo(35, "Throw", "Gramophone", 100, 1000);
        AddTaskInfo(36, "Points", "High", 500, 700);

        GetAllItemNames();
    }
    
    public static void AddTaskInfo(int tasknumber, string type, string itemName, int count, int reward)
    {
        tasks[tasknumber].taskNumber = tasknumber;
        tasks[tasknumber].type = type;
        tasks[tasknumber].itemName = itemName;
        tasks[tasknumber].count = count;
        tasks[tasknumber].reward = reward;
    }

    public static void GetAllItemNames()
    {
        foreach (TaskInfo task in tasks)
        {
            itemNames.Add(task.itemName);
        }
        
        foreach (string name in itemNames)
        {
            Debug.Log(name);
        }
    }
}
