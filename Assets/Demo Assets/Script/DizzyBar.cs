using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DizzyBar : MonoBehaviour
{
    public Slider Dizzyslider;
    public Animator CameraAnim;
    [SerializeField] int MaxDizzyValue = 100;

    public int CurrentDizzyValue = 0; //using for add value from another script

    // Start is called before the first frame update

    public void SetDizzyValue(int value)
    {
        CurrentDizzyValue += value;
        Dizzyslider.value += CurrentDizzyValue;
    }

    private void Start()
    {
        Dizzyslider.maxValue = MaxDizzyValue;
        Dizzyslider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Dizzyslider.value = CurrentDizzyValue;
        if (Dizzyslider.value < 25)
        {
            CameraAnim.SetInteger("Stage", 0);
        }
        if (Dizzyslider.value >= 25)
        {
            CameraAnim.SetInteger("Stage", 1);
        }
        if (Dizzyslider.value >= 50)
        {
            CameraAnim.SetInteger("Stage", 2);
        }
        if (Dizzyslider.value >= 75)
        {
            CameraAnim.SetInteger("Stage", 3);
        }
        if (Dizzyslider.value >= 100)
        {
            CameraAnim.SetInteger("Stage", 4);
        }       
    }

    public float getDizzyLevel()
    {
        return Dizzyslider.value;
    }

    public int getMaxDizzyLevel()
    {
        return MaxDizzyValue;
    }
}
