using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownController : MonoBehaviour
{
    public GameObject dropdownPanel; // Reference to the dropdown panel
    public float hoverHeight = 50f;  // The height from the top edge to trigger the dropdown
    private bool isDropdownVisible = false; // To keep track of the dropdown visibility

    void Start()
    {
        if (dropdownPanel == null)
        {
            Debug.LogError("Please assign a UI Panel to dropdownPanel.");
        }
        else
        {
            dropdownPanel.SetActive(false); // Initially hide the panel
        }
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        // Check if the mouse is at the top edge of the screen
        if (mousePosition.y >= Screen.height - hoverHeight)
        {
            if (!isDropdownVisible)
            {
                ShowDropdown();
            }
        }
        else
        {
            if (isDropdownVisible)
            {
                HideDropdown();
            }
        }
    }

    void ShowDropdown()
    {
        dropdownPanel.SetActive(true);
        isDropdownVisible = true;
    }

    void HideDropdown()
    {
        dropdownPanel.SetActive(false);
        isDropdownVisible = false;
    }
}
