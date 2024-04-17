using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureBar : MonoBehaviour
{
    public Slider slider;
    public float totalTime = 60f; // Total time for the countdown in seconds
    [SerializeField] float currentTime; // Current time in the countdown

    void Start()
    {
        currentTime = 0f; 
        slider.value = 0;
    }

    void Update()
    {
        if (slider != null)
        {
            // Update the current time
            currentTime += Time.deltaTime;
         
            float sliderValue = currentTime / totalTime;
              
            slider.value = sliderValue;
         
            if (currentTime >= totalTime)
            {
                currentTime = totalTime;
                // Optionally, you can add code here to handle what happens when the countdown reaches the total time
            }
        }
        else
        {
            Debug.LogWarning("Slider reference is missing!");
        }
    }
}
