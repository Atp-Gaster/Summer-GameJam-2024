using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    [SerializeField] Sprite GoodEnd;
    // Update is called once per frame   
    void Update()
    {        
        if (PlayerPrefs.GetInt("high_score") >= 250)
        {
            this.GetComponent<Image>().sprite = GoodEnd;
        }
    }
}
