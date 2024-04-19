using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] public List<RecipeSO> activeRecipes;
    [SerializeField] public CreateOrder CreateOrder;
    [SerializeField] public RecipeSO ambiguousRecipe;
    [SerializeField] public GameObject orderPrefab;
    
    
    public RecipeSO mix()
    {
        
        for (int i = 0; i < activeRecipes.Count; i++)
        {
            // Debug.Log(activeRecipes[i].ingredients.Count);
            // Debug.Log(dropPanel.ingredients.Count);
            if (activeRecipes[i].ingredients.Count == CreateOrder.ingredients.Count)
            {
                bool isEqual = true;
                for (int j = 0; j < CreateOrder.ingredients.Count; j++)
                {
                    // Debug.Log(dropPanel.ingredients[j].ToString());
                    // Debug.Log(activeRecipes[i].ingredients[j].ToString());
                    if (activeRecipes[i].ingredients[j] != CreateOrder.ingredients[j])
                    {
                        isEqual = false;
                        break;
                    }
                }
                if (isEqual)
                {
                    CreateOrder.clearIngredients();
                    return activeRecipes[i];
                }
            }
        }
        CreateOrder.clearIngredients();
        return ambiguousRecipe;
    }

    public void generateMixCocktail()
    {        
        if(CreateOrder.TotalIngrediant > 1)
        {
            GameObject cocktail = Instantiate(orderPrefab);
            cocktail.GetComponent<order>().cocktail = mix();
            cocktail.GetComponent<SpriteRenderer>().sprite = mix().RecipeImage;
        }       
    }

    public void stringMix()
    {
        Debug.Log(mix().recipeName);
    }
}
