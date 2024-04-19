using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class CreateOrder : MonoBehaviour
{
    [Header("Order Parameter")]    
    public List<IngredientSO> ingredients = new List<IngredientSO>();    
    public bool IsIce = false;
    public bool IsBoba = false;
    public bool IsLime = false;

    [Header("Order Property")]
    public int TotalIngrediant = 0;
    [SerializeField] int MaxIngrediant = 5;
    [SerializeField] bool IsFull = false;

    [Header("Sprite Copy Settings")]  
    public GameObject ImagePrefab; // Reference to the GameObject where sprite will be copied
    public GameObject ContentOverlay;    

    void LoadResourcesPicture(int ID)
    {
        ImagePrefab = Resources.Load<GameObject>("Icon/" + ID.ToString());        
        GameObject newImageObject = Instantiate(ImagePrefab, ContentOverlay.transform);
    }
    public void clearIngredients()
    {
        ingredients.Clear();

        foreach (Transform child in ContentOverlay.transform)
        {
            Destroy(child.gameObject);
        }

        TotalIngrediant = 0;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(TotalIngrediant < MaxIngrediant)
        {
            if (collision.tag == "Tonic" || collision.tag == "Toping")
            {
                TotalIngrediant += 1;
                int tempID = collision.GetComponent<Ingredient>().ID;                
                ingredients.Add(collision.gameObject.GetComponent<Ingredient>().ingredient);
                LoadResourcesPicture(tempID);
            }
        }               
    }

    private void Update()
    {
       
    }

}
