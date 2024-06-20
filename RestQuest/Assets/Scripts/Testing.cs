using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleCalendarUnity;

public class Testing : MonoBehaviour
{
    public GameObject CalendarText; 

    public void OpenPanel()
    {
        if(CalendarText != null)
        {

            bool isActive = CalendarText.activeSelf;

            CalendarText.SetActive(!isActive);
        }
        
    }
    
    
    
    public Calendar calendar = new Calendar();
    
    private void Update()
    {
        calendar.Main();
    }

}
