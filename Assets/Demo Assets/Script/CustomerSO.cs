using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "New Customer", menuName = "Customer")]
public class CustomerSO : ScriptableObject
{
    public string customerName;
    public Sprite customerSprite;
    public float timerDuration; // Time before customer leaves
    public CocktailInfoSO[] wishlistItems; // Array of wishlist items
    public int[] state;
    public int currentState;

    public string order;
    public Sprite orderSprite;

    public CocktailInfoSO item;

    public CocktailInfoSO GetRandomWishlist()
    {
        if (wishlistItems.Length == 0)
        {
            Debug.LogError("wishlistItems array is empty!");
            return null;
        }

        CocktailInfoSO randomItem = wishlistItems[Random.Range(0, wishlistItems.Length)];
        order = randomItem.Name;
        orderSprite = randomItem.Sprite;
        item = randomItem;
        // Debug.Log("Wishlist item name: " + randomItem.itemName);
        return randomItem;
    }
}
