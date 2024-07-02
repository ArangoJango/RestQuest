using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    void Start()
    {
        Snoozed = false;
        Playtime = 0f;
        BreakDelay = 30f;         // sollte theoretisch anpassbar sein
        BreakIntervalSeconds = BreakInterval * 60;
        AppointmentAheadSeconds = AppointmentAhead * 60;
        //StartCoroutine(Playtime());
        //Empty Queue 
        // Get Todays-Apointments
        // Add all of them to the queue

    }

    public TMP_Text MessageTitle;
    public TMP_Text MessageText;
    // public TMP_Text SnoozedMessageTitle;
    public TMP_Text SnoozedMessageText;

    public GameObject NotificationWindow;
    public GameObject SnoozeWindow;

    private string Appointment = "Appointment";
    private string Break = "Take a break";

    private bool Snoozed;
    private bool AppointNext;

    //private Queue<string> popupQue;
    //private Coroutine queueChecker;

    private float Playtime;
    [Range(5, 60)] public int BreakInterval;
    [Range(5, 60)] public int AppointmentAhead;
    private float BreakIntervalSeconds;
    private float AppointmentAheadSeconds;
    private float BreakDelay;
    private float BreakTime;
    private float AppointmentClose;

    private void ShowBreakNotif()
    {
        if (Snoozed == false)
        {
            NotificationWindow.SetActive(true);
            MessageTitle.text = Break;
            StartCoroutine(BreakTimeCount());
            // MessageText.text = variable mit der info;

        }
        else
        {
            SnoozeWindow.SetActive(true);
            // SnoozedMessageTitle.text = Break;
            StartCoroutine(BreakTimeCount());
            // SnoozedMessageText.text = variable mit der info;
        }
    }

    private void ShowAppointmentNotif()
    {
        if (Snoozed == false)
        {
            NotificationWindow.SetActive(true);
            MessageTitle.text = Appointment;
            StartCoroutine(BreakTimeCount());
            // MessageText.text = variable mit der info;

        }
        else
        {
            SnoozeWindow.SetActive(true);
            // SnoozedMessageTitle.text = Appointment;
            StartCoroutine(BreakTimeCount());
            // SnoozedMessageText.text = variable mit der info;
        }
    }

    public void Snooze()
    {
        Snoozed = true;
        NotificationWindow.SetActive(false);
        SnoozeWindow.SetActive(false);
        if (AppointNext == false)
        {
            StartCoroutine(DelayMessage());
        }
        if (AppointNext == true)
        {
            StartCoroutine(DelayAppointment());
        }
        StopCoroutine(BreakTimeCount());
        BreakTime = 0f;
    }

    public void Acknowledged()
    {
        Snoozed = false;
        NotificationWindow.SetActive(false);
        SnoozeWindow.SetActive(false);
        // delete message from queue
        StopCoroutine(BreakTimeCount());
        BreakTime = 0f;
        Playtime = 0f;
    }


    private IEnumerator BreakTimeCount()
    {
        while (BreakTime <= 300)
        {
            BreakTime += 1;
            yield return new WaitForSeconds(1);
        }

        if (BreakTime >= 30)
        {
            /* for (kps <= 0)
             * {
             * yield return new WaitForSeconds(1);
             * } */
            Acknowledged();
        }

    }

    private IEnumerator DelayMessage()
    {
        while (BreakDelay >= 0)
        {
            BreakDelay -= 1;
            yield return new WaitForSeconds(1);
        }
        ShowBreakNotif();
        BreakDelay = 300f;
        StopCoroutine(DelayMessage());
    }

    private IEnumerator DelayAppointment()
    {
        while (BreakDelay >= 0)
        {
            BreakTime -= 1f;
            yield return new WaitForSeconds(1);
        }
        ShowAppointmentNotif();
        BreakDelay = 300f;
        StopCoroutine(DelayAppointment());
    }
    //Playtime hochzählen

    /* public void AddToQueue(string text)
    {
        popupQue.Enqueue(text);
        //unfinished
    } */
}


