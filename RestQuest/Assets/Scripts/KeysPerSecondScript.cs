using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeysPerSecondScript : MonoBehaviour
{
    public TMP_Text keysPerSecondText;  // Reference to a UI Text component to display the result
    private int keyPressCount = 0;  // To count the number of key presses
    private float elapsedTime = 0f; // To track the elapsed time

    private float maxKPS = 20f; // Maximum KPS for color scaling.

    void Start()
    {
        if (keysPerSecondText == null)
        {
            Debug.LogError("Please assign a UI Text component to keysPerSecondText.");
        }
    }

    void Update()
    {
        // Check if any key is pressed
        if (Input.anyKeyDown)
        {
            keyPressCount++;
        }

        // Update the elapsed time
        elapsedTime += Time.deltaTime;

        // Calculate keys per second
        if (elapsedTime >= 1f)
        {
            float keysPerSecond = keyPressCount / elapsedTime;

            // Display the keys per second on the UI
            if (keysPerSecondText != null)
            {
                keysPerSecondText.text = "KPS: " + keysPerSecond.ToString("F2");
                keysPerSecondText.color = Color.Lerp(Color.green, Color.red, Mathf.Clamp01(keysPerSecond / maxKPS));
            }

            // Reset the counters
            keyPressCount = 0;
            elapsedTime = 0f;
        }
    }
}