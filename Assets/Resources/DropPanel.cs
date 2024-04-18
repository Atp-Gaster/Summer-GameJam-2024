using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPanel : MonoBehaviour
{
    public List<IngredientSO> ingredients = new List<IngredientSO>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ingredients.Add(collision.gameObject.GetComponent<Ingredient>().ingredient);
        Debug.Log("Added " + collision.gameObject.GetComponent<Ingredient>().ingredient.ToString());
    }

    public void clearIngredients()
    {
        ingredients.Clear();
    }
}
