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
        BreakDelay = 300f;         // sollte theoretisch anpassbar sein, aber da wir kein User-Optionsmenü haben, habe ich es fix gemacht
        BreakIntervalSeconds = BreakInterval * 60; // Interval in which break times show in Seconds
        AppointmentAheadSeconds = AppointmentAhead * 60; // Premonition-time for Appointments in Seconds

    }

    public TMP_Text MessageTitle;
    public TMP_Text MessageText;
    // public TMP_Text SnoozedMessageTitle;
    public TMP_Text SnoozedMessageText;

    private TMP_Text KPS_UI_Element;

    public GameObject NotificationWindow;
    public GameObject SnoozeWindow;

    private string Appointment = "Appointment";
    private string Break = "Take a break";

    private bool Snoozed; // Tracks if the next upcoming reminder shows up for the first time or not
    private bool AppointNext; // True if an appointment is the next reminder, false if the next reminder is about taking a break

    //private Queue<string> popupQue;

    private float Playtime;
    [Range(2, 60)] public int BreakInterval;
    [Range(5, 60)] public int AppointmentAhead;
    private float BreakIntervalSeconds;
    private float AppointmentAheadSeconds;
    private float BreakDelay;
    private float BreakTime;
    private float AppointmentClose;


    // Coroutine that checks which Notification comes next

    /*
    private void QueueChecker
    {
        check: next in queue appointment or break (what happens sooner)
        

        if break
            while (playtime <= (BreakInterval*0,9))
                return
            while (platime > (BreakInterval*0,9)
                if(playtime <= (BreakInterval*1.3)
                    check if cutscene
                    while cutsczene = true 
                        return
                    else
                        check if kps low
                        while kps low
                            ShowBreakNotif()
                else
                    check if kps low
                    while kps low
                        ShowBreakNotif()
            

        if appointment
             while (playtime <= (AppointmentAhead*0,7)
                return
             while (platime > (AppointmentAhead*0,7)
                if(playtime <= (AppointmentAhead*0,1)
                    check if cutscene
                    while cutsczene = true 
                        return
                    else
                        check if kps low
                        while kps low
                            ShowAppointNotif()
                else
                   ShowAppointNotif()

    }
  
     Appointment Notifications sollen beim letztmöglichen sinnvollen Zeitpunkt immer gezeigt werden, bei Pausenremindern wird noch nach Spieleraktivität geprüft.

    Was komplett fehlt ist eine Funktion die Calendar Events in ne Queue zu packen, sodass die Funktion vergleichen kann, ob das erste Item in der Queue, oder die Pause zuerst passiert.
     */

    // BreakNotification
    private void ShowBreakNotif()
    {
        if (Snoozed == false)
        {
            // Kleines Fenster
            NotificationWindow.SetActive(true);
            MessageTitle.text = Break;
            StartCoroutine(BreakTimeCount());
            MessageText.text = "Remember to drink some water.";// Besser wäre variable und Message-Varianten, habe die aber wieder rausgenommen.

        }
        else
        {
            // Großes Fenster
            SnoozeWindow.SetActive(true);
            // SnoozedMessageTitle.text = Break;
            StartCoroutine(BreakTimeCount());
            SnoozedMessageText.text = "Remember to drink some water.";
        }
    }


    // Appointment Notification
    private void ShowAppointmentNotif()
    {
        // private int TimeLeft = (AppointmentTime - CurrentTime) in Minutes
         SnoozeWindow.SetActive(true); // Appointments always have the big window
         StartCoroutine(BreakTimeCount());
            // MessageText.text = "Your Appointment starts in" + TimeLeft + "Minutes";
    }

    // Function for Snooze Button
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


    // Function Got the message and understood.
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


    // Automatic Break Recognition if Player away
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

    // Delay Message for X Seconds
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


    // Delay Appointment Notification for X Seconds
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

    private void Update()
    {
        Playtime += Time.deltaTime; //Active Playtime

        //QueueChecker(); 
    }

}


