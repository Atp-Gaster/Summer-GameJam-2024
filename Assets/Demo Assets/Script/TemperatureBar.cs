using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureBar : MonoBehaviour
{
    public Slider slider;
    public float totalTime = 60f; // Total time for the countdown in seconds
    [SerializeField] public float currentTime; // Current time in the countdown
    [SerializeField] Image PlayerPicture;
    [SerializeField] Sprite []IconSprite;
    [SerializeField] Animator CameraAnim;


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

        if (currentTime < 25)
        {            
            PlayerPicture.sprite = IconSprite[0];
        }
        if (currentTime >= 25)
        {
            PlayerPicture.color = Color.yellow;
            PlayerPicture.sprite = IconSprite[1];
        }
        if (currentTime >= 50)
        {
            PlayerPicture.color = new Color(1.0f, 0.64f, 0.0f);
            PlayerPicture.sprite = IconSprite[2];
        }
        if (currentTime >= 75)
        {
            PlayerPicture.color = Color.red;
            PlayerPicture.sprite = IconSprite[3];
        }
    }
}
