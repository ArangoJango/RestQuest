using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetLocalTime : MonoBehaviour
{

    public TMP_Text TimeOutput; 
    // Start is called before the first frame update
    void Start()
    {
        TimeOutput.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        TimeOutput.text = System.DateTime.Now.ToString("HH:mm dd MMMM, yyyy");
    }
}
