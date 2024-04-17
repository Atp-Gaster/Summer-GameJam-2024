using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] public List<RecipeSO> activeRecipes;
    [Header("Test")]
    public List<IngredientSO> testActiveIngredient;

    void Start()
    {
        Debug.Log(matchRecipeWithIngredients(testActiveIngredient).recipeName);
    }

    public RecipeSO matchRecipeWithIngredients(List<IngredientSO> ingredients)
    {
        for (int i = 0; i < activeRecipes.Count; i++)
        {
            if (activeRecipes[i].ingredients.Count == ingredients.Count)
            {
                bool isEqual = true;
                for (int j = 0; j < ingredients.Count; j++)
                {
                    if (activeRecipes[i].ingredients[j] != ingredients[j])
                    {
                        isEqual = false;
                        break;
                    }
                }
                if (isEqual)
                {
                    return activeRecipes[i];
                }
            }
        }
        return null;
    }

    public string matchRecipeWithIngredientsTest()
    {
        for (int i = 0; i < activeRecipes.Count; i++)
        {
            if (activeRecipes[i].ingredients.Count == testActiveIngredient.Count)
            {
                bool isEqual = true;
                for (int j = 0; j < testActiveIngredient.Count; j++)
                {
                    if (activeRecipes[i].ingredients[j] != testActiveIngredient[j])
                    {
                        isEqual = false;
                        break;
                    }
                }
                if (isEqual)
                {
                    return activeRecipes[i].recipeName;
                }
            }
        }
        return "No Recipe Found";
    }
}
