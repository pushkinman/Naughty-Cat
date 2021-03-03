using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotificationSamples;

public class NotificationsManager : MonoBehaviour
{
    public GameNotificationsManager notificationsManager;

    private void Start()
    {
        InitializeNotifications();
        //CreateNotification("We miss you!", "Come back and collect your rewards", DateTime.Now.AddSeconds(5));
    }

    public void CreateNotification(float time)
    {
        CreateNotification("Chest is Ready!", "Come and collect your reward", DateTime.Now.AddMilliseconds(time));
    }

    private void InitializeNotifications()
    {
        GameNotificationChannel channel = new GameNotificationChannel("ComeBack", "name", "description", GameNotificationChannel.NotificationStyle.Default);
        notificationsManager.Initialize(channel);
    }

    private void CreateNotification(string title, string body, DateTime time)
    {
        IGameNotification notification = notificationsManager.CreateNotification();
        if (notification != null)
        {
            notification.Title = title;
            notification.Body = body;
            notification.DeliveryTime = time;
            notification.SmallIcon = "icon_0";
            notification.LargeIcon = "icon_1";
            notificationsManager.ScheduleNotification(notification);

        }
    }
}
