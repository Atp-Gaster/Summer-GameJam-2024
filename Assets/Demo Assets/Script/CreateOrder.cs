using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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

    [Header("Sprite Copy Settings")]
    public Sprite IngrediantPrefab;  // Prefab of the sprite object to be copied
    public GameObject ImagePrefab; // Reference to the GameObject where sprite will be copied
    public GameObject ContentOverlay;
    private void CopySpriteToTarget(GameObject original)
    {
        // Instantiate the ImagePrefab and copy sprite from original to it
        GameObject newImageObject = Instantiate(ImagePrefab, ContentOverlay.transform);
        SpriteRenderer originalSpriteRenderer = original.GetComponent<SpriteRenderer>();
        newImageObject.GetComponent<Image>().sprite = originalSpriteRenderer.sprite;     
    }


    void LoadResourcesPicture(int ID)
    {
        ImagePrefab = Resources.Load<GameObject>("Icon/" + ID.ToString());        
        GameObject newImageObject = Instantiate(ImagePrefab, ContentOverlay.transform);
    }


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
            if (TotalIngrediant < MaxIngrediant)
            {
                TotalIngrediant += 1;
                //CopySpriteToTarget(collision.gameObject);
                LoadResourcesPicture(collision.GetComponent<Tonic>().TonicID);
            }

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
                case "Ice": IsIce = true;
                    //CopySpriteToTarget(collision.gameObject);
                    LoadResourcesPicture(collision.GetComponent<Toping>().TopingID);
                    break;
                case "Lime": IsLime = true;
                    // CopySpriteToTarget(collision.gameObject); 
                    LoadResourcesPicture(collision.GetComponent<Toping>().TopingID);
                    break;
                case "Boba": IsBoba = true;
                    // CopySpriteToTarget(collision.gameObject); 
                    LoadResourcesPicture(collision.GetComponent<Toping>().TopingID);
                    break;
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
