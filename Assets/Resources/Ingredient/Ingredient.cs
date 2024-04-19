using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public int ID = -99;
    public IngredientSO ingredient;

    private void Start()
    {
        ID = ingredient.ingredientID;
    }
}
