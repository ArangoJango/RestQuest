using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotifOld : MonoBehaviour
{
    public TMP_Text BreakMessage;
    public TMP_Text AppointmentMessage;

    public GameObject BreakWindow;
    public GameObject AppointmentWindow;

    private Queue<string> popupQue;
    private Coroutine queueChecker;

    public void AddToQueue(string text)
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
    }


    void Start()
    {
        //Empty Queue 
        // Get Todays-Apointments
        // Add all of them to the queue
    }

    // Update is called once per frame

