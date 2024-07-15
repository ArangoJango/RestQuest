using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Redo_Notification_Script : MonoBehaviour
{
    void Start()
    {
        Snoozed = false;
        Playtime = 0f;
        BreakDelay = 300f;
        BreakIntervalSeconds = BreakInterval * 60;
        AppointmentAheadSeconds = AppointmentAhead * 60;
        eventQueue = new Queue<CalendarEvent>();

        StartCoroutine(QueueChecker());
    }

    public TMP_Text MessageTitle;
    public TMP_Text MessageText;
    public TMP_Text SnoozedMessageText;
    private TMP_Text KPS_UI_Element;

    public GameObject NotificationWindow;
    public GameObject SnoozeWindow;

    private string Appointment = "Appointment";
    private string Break = "Take a break";

    private bool Snoozed;
    private bool AppointNext;

    private Queue<CalendarEvent> eventQueue; //EventListe mit den Appointments + Breaks

    private float Playtime;
    [Range(2, 60)] public int BreakInterval;
    [Range(5, 60)] public int AppointmentAhead;
    private float BreakIntervalSeconds;
    private float AppointmentAheadSeconds;
    private float BreakDelay;
    private float BreakTime;
    private float AppointmentClose;

    private void Update()
    {
        Playtime += Time.deltaTime;

        // Check for input to trigger events
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TriggerCalendarEvent();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TriggerBreakEvent();
        }
    }

    private void TriggerCalendarEvent()
    {
        // Example: Trigger a calendar event 5 minutes from now
        DateTime eventTime = DateTime.Now.AddMinutes(5);
        AddCalendarEvent(eventTime, "Sample Appointment");
    }

    private void TriggerBreakEvent()
    {
        // Example: Trigger a break event now
        AddBreakEvent();
    }

    public void AddCalendarEvent(DateTime eventTime, string eventName)
    {
        eventQueue.Enqueue(new CalendarEvent { EventTime = eventTime, EventName = eventName });
    }

    public void AddBreakEvent()
    {
        // Add a break event to the queue immediately
        Playtime += BreakIntervalSeconds; // Enable Correct Notification Window
        // eventQueue.Enqueue(new CalendarEvent { EventTime = DateTime.Now, EventName = Break });

    }

    // Hier der QueueChecker
    private IEnumerator QueueChecker()
    {
        while (true)
        {
            if (eventQueue.Count > 0)
            {
                var nextEvent = eventQueue.Peek();
                var timeToNextEvent = (float)(nextEvent.EventTime - DateTime.Now).TotalSeconds;

                if (timeToNextEvent <= AppointmentAheadSeconds)
                {
                    ShowAppointmentNotif(nextEvent);
                    eventQueue.Dequeue();
                }
                else if (Playtime >= BreakIntervalSeconds)
                {
                    ShowBreakNotif();
                    Playtime = 0f;
                }
            }
            else if (Playtime >= BreakIntervalSeconds)
            {
                ShowBreakNotif();
                Playtime = 0f;
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void ShowBreakNotif()
    {
        if (!Snoozed)
        {
            NotificationWindow.SetActive(true);
            MessageTitle.text = Break;
            StartCoroutine(BreakTimeCount());
            MessageText.text = "Remember to drink some water.";
        }
        else
        {
            SnoozeWindow.SetActive(true);
            StartCoroutine(BreakTimeCount());
            SnoozedMessageText.text = "Remember to drink some water.";
        }
    }

    private void ShowAppointmentNotif(CalendarEvent calendarEvent)
    {
        SnoozeWindow.SetActive(true);
        StartCoroutine(BreakTimeCount());
        string theTime = calendarEvent.EventTime.ToString("HH:mm");
        SnoozedMessageText.text = $"{calendarEvent.EventName} starts at {theTime}.";
    }

    public void Snooze()
    {
        Snoozed = true;
        NotificationWindow.SetActive(false);
        SnoozeWindow.SetActive(false);
        if (!AppointNext)
        {
            StartCoroutine(DelayMessage());
        }
        else
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
    }

    private IEnumerator DelayAppointment()
    {
        while (BreakDelay >= 0)
        {
            BreakTime -= 1f;
            yield return new WaitForSeconds(1);
        }
        if (eventQueue.Count > 0)
        {
            ShowAppointmentNotif(eventQueue.Dequeue());
        }
        BreakDelay = 300f;
    }
    //Eventklasse zum storen von CalenderEvents
    private class CalendarEvent
    {
        public DateTime EventTime { get; set; }
        public string EventName { get; set; }
    }
}
