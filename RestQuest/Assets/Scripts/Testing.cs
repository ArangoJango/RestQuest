using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleCalendarUnity;

public class Testing : MonoBehaviour
{
    public GameObject CalendarText, CalenderUI; 

    public void OpenPanel()
    {
        if(CalendarText != null)
        {

            bool isActive = CalendarText.activeSelf;

            CalendarText.SetActive(!isActive);
        }
        
    }
    
    
    
    public Calendar calendar = new Calendar();
    
    /*private void Update()
    {
        calendar.Main();
    }*/

    private void Start()
    {
        calendar.Main();
    }

    public void UpdateCalendar()
    {
        calendar.Main();
    }
    public void CloseCalendar()
    {
        CalenderUI.SetActive(false);
    }
}
