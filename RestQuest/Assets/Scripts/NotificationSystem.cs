using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleCalendarUnity;
using TMPro;
using System;
using UnityEngine.Events;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class NotificationSystem : MonoBehaviour
{
    [SerializeField] public static string TimefromAppointment;
    [SerializeField] private TMP_Text calendarText;
    [SerializeField] private GameObject calendarUI;
    [SerializeField] private GameObject drinkReminderUI;
    [SerializeField] private TMP_Text KPS_UI_Element;
    [SerializeField] private float reminderDelay = 5f; // In-game time delay in seconds before allowing reminder activation
    [SerializeField] private float drinkReminderDelay = 1800f; // In-game time delay in seconds (30 minutes) before showing drink reminder

    private CalendarService calendarService;
    private string[] scopes = { CalendarService.Scope.CalendarReadonly };
    private bool waitingForReminder = false;
    private float lastKeyPressTime = 0f;

    private async void Start()
    {
        UserCredential credential;

        // Google Calendar API setup
        using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
        {
            string credPath = "token.json";
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true));
        }

        // Create Calendar service
        calendarService = new CalendarService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "Google Calendar Unity Integration",
        });

        // Load and display calendar events for today
        UpdateCalendarText();
    }

    private async void UpdateCalendarText()
    {
        DateTime now = DateTime.Now.Date;
        DateTime tomorrow = now.AddDays(1).Date;

        EventsResource.ListRequest request = calendarService.Events.List("primary");
        request.TimeMin = now;
        request.TimeMax = tomorrow;
        request.SingleEvents = true;
        request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

        Events events = await request.ExecuteAsync();
        string eventSummary = "";

        foreach (var eventItem in events.Items)
        {
            string summary = eventItem.Summary;
            string dateTimeString = "";

            // Check if start time is specified
            if (eventItem.Start.DateTime != null)
            {
                DateTime eventTime = (DateTime)eventItem.Start.DateTime;
                dateTimeString = eventTime.ToString("dd.MM.yyyy HH:mm");
            }
            else
            {
                dateTimeString = "No specific start time";
            }

            eventSummary += $"Event Summary: {summary}\n";
            eventSummary += $"Date and Time: {dateTimeString}\n\n";
            TimefromAppointment = dateTimeString;
        }

        calendarText.text = eventSummary;
    }

    public void CheckReminders()
    {
        // Check if enough time has passed since last key press
        if (Time.time - lastKeyPressTime < reminderDelay)
        {
            // Wait until player's KPS is under 5
            waitingForReminder = true;
            return;
        }

        // Check Google Calendar events and activate UI element if conditions met
        Events events = calendarService.Events.List("primary").Execute();
        DateTime currentTime = DateTime.Now;

        foreach (var eventItem in events.Items)
        {
            DateTime eventTime = DateTime.Parse(eventItem.Start.DateTimeRaw);

            if (eventTime == currentTime)
            {
                // Activate UI element for reminder
                calendarUI.SetActive(true);
                // Hier Button oder andere Aktionen bei Übereinstimmung mit der aktuellen Zeit ausführen
            }
        }
    }

    public void ActivateDrinkReminder()
    {
        // Activate drink reminder UI after specified delay
        Invoke("ShowDrinkReminder", drinkReminderDelay);
    }

    private void ShowDrinkReminder()
    {
        drinkReminderUI.SetActive(true);
    }

    // Beispiel für das Handling der KeyPressRate und Tastenabfrage
    private void Update()
    {
        // Tastenabfrage für Test-Timer-Event (Taste 1)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetTestTimerEvent();
        }

        // Tastenabfrage für Drink Reminder (Taste 2)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateDrinkReminder();
        }

        // Key Press Rate Überprüfung für Reminder-System
        if (Input.anyKeyDown)
        {
            lastKeyPressTime = Time.time;

            if (waitingForReminder && GetKeyPressRate() <= 5)
            {
                CheckReminders();
                waitingForReminder = false;
            }
        }
    }

    private float GetKeyPressRate()
    {
        // Lese die KPS aus dem UI-Element
        return float.Parse(KPS_UI_Element.text);
    }

    private void SetTestTimerEvent()
    {
        // Beispiel für die Ausführung eines Test-Timer-Events
        DateTime testEventTime = DateTime.Now.AddSeconds(10); // Timer Event in 10 Sekunden

        // Hier können Sie weitere Aktionen für das Test-Timer-Event ausführen
        Debug.Log($"Test Timer Event set for: {testEventTime}");
    }
}