using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public TemperatureBar TemperatureBar;
    [SerializeField] public DizzyBar DizzyBar;
    public RecipeSO[] wishlistItems;
    public RecipeSO currentItems;
    public Sprite[] playerStates;

    private Image playerImage;

    // Start is called before the first frame update
    void Start()
    {
        playerImage = player.GetComponent<Image>();
    }

    public void CalculateTempChangeState()
    {
        if (playerImage == null || playerStates.Length == 0 || player.GetComponent<Image>() == null)
        {
            Debug.LogWarning("Player Image or payerStates array is not set.");
            return;
        }

        if (TemperatureBar.currentTime < 25)
        {
            playerImage.sprite = playerStates[0];
        }
        else if (TemperatureBar.currentTime > 25 && TemperatureBar.currentTime < 50)
        {
            playerImage.sprite = playerStates[1];
        }
        else if (TemperatureBar.currentTime > 50 && TemperatureBar.currentTime < 75)
        {
            playerImage.sprite = playerStates[2];
        }
        else
        {
            playerImage.sprite = playerStates[3];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentItems == null)
        {
            Debug.Log("cocktails add");
            currentItems = GetRandomCocktail();
        }

        CalculateTempChangeState();
    }

    public RecipeSO GetRandomCocktail()
    {
        if (wishlistItems.Length == 0)
        {
            Debug.Log("No cocktails available.");
            return null;
        }

        int randomIndex = Random.Range(0, wishlistItems.Length);
        RecipeSO randomCocktail = wishlistItems[randomIndex];

        Image itemimage = player.transform.GetChild(0).GetComponent<Image>();
        itemimage.sprite = randomCocktail.RecipeImage;

        return randomCocktail;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cocktail")
        {
            order cocktail = collision.GetComponent<order>();
            if (cocktail != null && cocktail.cocktail == currentItems)
            {
                currentItems = null;
                TemperatureBar.currentTime = TemperatureBar.currentTime - 10;
                DizzyBar.CurrentDizzyValue = DizzyBar.CurrentDizzyValue + 2;

                if (TemperatureBar.currentTime < 0)
                {
                    TemperatureBar.currentTime = 0;
                }
                if (DizzyBar.CurrentDizzyValue < 0)
                {
                    DizzyBar.CurrentDizzyValue = 0;
                }
            }
            else
            {
                DizzyBar.CurrentDizzyValue = DizzyBar.CurrentDizzyValue + 10;
                Debug.Log("incorrect cocktail");
            }
        }
    }
}
