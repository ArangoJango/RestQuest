using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocationProvider : MonoBehaviour
{
    public TMP_Text LocationText;
    public string LocationName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LocationText.text = LocationName;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        LocationText.text = "";
    }
}
