using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum CupSize
{
    Small, Medium, Big
}
public class CreateOrder : MonoBehaviour
{
    [Header("Order Parameter")]
    public Dictionary<string, int> TonicCount = new Dictionary<string, int>();  
    public CupSize cupSize = CupSize.Medium;      
    public bool IsIce = false;
    public bool IsBoba = false;
    public bool IsLime = false;

    [Header("Order Property")]
    [SerializeField] int TotalIngrediant = 0;
    [SerializeField] int MaxIngrediant = 0;
    [SerializeField] bool IsFull = false;

    private void Start()
    {
        TonicCount.Add("Red", 0);
        TonicCount.Add("Blue", 0);
        TonicCount.Add("Green", 0);
        TonicCount.Add("Shit", 0);
    }   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Tonic")
        {           
            if(TotalIngrediant < MaxIngrediant) TotalIngrediant += 1;
            else IsFull = true;

            foreach (string EachTonic in TonicCount.Keys.ToList())
            {
                if (collision.GetComponent<Tonic>().Name == EachTonic && !IsFull) TonicCount[EachTonic] += 1;
                Debug.Log(EachTonic.ToString() + " has: " + TonicCount[EachTonic].ToString());
                Debug.Log("================");
            }            
        }

        if (collision.tag == "Toping")
        {
            Toping PlayerTopping = collision.GetComponent<Toping>();
            switch(PlayerTopping.Name)
            {
                case "Ice": IsIce = true; break;
                case "Lime": IsLime = true; break;
                case "Boba": IsBoba = true; break;
            }
        }
    }

    private void Update()
    {
        switch (cupSize)
        {
            case CupSize.Small:
                MaxIngrediant = 5;
                break;
            case CupSize.Medium:
                MaxIngrediant = 10;
                break;
            case CupSize.Big:
                MaxIngrediant = 15;
                break;
        }
    }

}
