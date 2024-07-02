using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationManager : MonoBehaviour
{
   public static NotificationManager Instance
    {
        get
        {
            if(instance != null)
            {
                return instance;
            }

            instance = FindObjectOfType<NotificationManager>();

            if (instance != null)
            {
                return instance;
            }

            CreateNewInstance();

            return instance;
        }
    }

    private static NotificationManager CreateNewInstance()
    {
        NotificationManager notificationManagerPrefab = Resources.Load<NotificationManager>("NotificationManager"); 
        instance = Instantiate(notificationManagerPrefab);

        return instance;
    }

    private static NotificationManager instance;

    private void Awake()
    {
        if(Instance =! this)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private TMP_Text notificationText;
    [SerializeField] private float showTime;

    public void SetNewNotification()

}
