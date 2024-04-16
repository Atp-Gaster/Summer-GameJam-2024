using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class CookManager : MonoBehaviour
{
    public CreateOrder Order;

    public CocktailInfoSO[] CocktailList; //using to store Cocktail Receipt

    [SerializeField] Text DemoText;

    private void Start()
    {
        DemoText.text = "";
    }
    public void CheckOrder()
    {
        DemoText.text = string.Format("<color=#00FF00><size=30>Current Mixed</size></color>\n");
        foreach (string EachTonic in Order.TonicCount.Keys)
        {            
            DemoText.text += string.Format("{0} has: {1}\n", EachTonic.ToString(), Order.TonicCount[EachTonic].ToString());
        }

        if(Order.IsIce) DemoText.text += string.Format("\n And With Ice \n");
        if (Order.IsLime) DemoText.text += string.Format("\n And With Lime \n");
        if (Order.IsBoba) DemoText.text += string.Format("\n And With Boba \n");
    }
}
