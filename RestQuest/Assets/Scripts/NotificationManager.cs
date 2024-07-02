using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    void Start()
    {
        //Empty Queue 
        // Get Todays-Apointments
        // Add all of them to the queue
    }

    public TMP_Text MessageTitle;
    public TMP_Text MessageText;
    public TMP_Text SnoozedMessageTitle;
    public TMP_Text SnoozedMessageText;

    public GameObject NotificationWindow;
    public GameObject SnoozeWindow;

    private string Appointment = "Appointment";
    private string Break = "Take a break";

    //private Queue<string> popupQue;
    //private Coroutine queueChecker;

    private float Playtime;
    public float BreakInterval;

    //Playtime hochzählen

    /* public void AddToQueue(string text)
    {
        popupQue.Enqueue(text);
        //unfinished
    }

    private void ShowBreakPopup(string text)
    {
        BreakWindow.SetActive(true);
        BreakMessage.text = text;
        //unfinished
    }

    private void ShowAppointmentMessage(string text)
    {
        AppointmentWindow.SetActive(true);
        AppointmentMessage.text = text;
        //unfinished
    } */




}
