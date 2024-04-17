using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObject/Recipe", order = 3)]
public class RecipeSO : ScriptableObject
{
    [SerializeField]
    public string recipeName;
    [SerializeField]
    public List<IngredientSO> ingredients = new List<IngredientSO>();
    [SerializeField] public bool isOrderSensitive = false;
    [SerializeField] public bool isAlcoholic {get; private set;}
    


    public void Awake()
    {
        if (ingredients != null && ingredients.Count > 0)
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                if (ingredients[i] != null && ingredients[i].isAlcoholic)
                {
                    this.isAlcoholic = true;
                }
            }
        }
    }
}
