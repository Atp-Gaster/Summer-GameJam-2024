using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] public List<RecipeSO> activeRecipes;
    [SerializeField] public DropPanel dropPanel;
    [SerializeField] public RecipeSO ambiguousRecipe;
    
    
    public RecipeSO mix()
    {
        
        for (int i = 0; i < activeRecipes.Count; i++)
        {
            // Debug.Log(activeRecipes[i].ingredients.Count);
            // Debug.Log(dropPanel.ingredients.Count);
            if (activeRecipes[i].ingredients.Count == dropPanel.ingredients.Count)
            {
                bool isEqual = true;
                for (int j = 0; j < dropPanel.ingredients.Count; j++)
                {
                    // Debug.Log(dropPanel.ingredients[j].ToString());
                    // Debug.Log(activeRecipes[i].ingredients[j].ToString());
                    if (activeRecipes[i].ingredients[j] != dropPanel.ingredients[j])
                    {
                        isEqual = false;
                        break;
                    }
                }
                if (isEqual)
                {
                    dropPanel.clearIngredients();
                    return activeRecipes[i];
                }
            }
        }
        dropPanel.clearIngredients();
        return ambiguousRecipe;
    }

    public void stringMix()
    {
        Debug.Log(mix().recipeName);
    }
}
