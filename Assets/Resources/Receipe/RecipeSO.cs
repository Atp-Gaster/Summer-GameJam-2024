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

    [SerializeField] public Sprite RecipeImage;   

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

    private void OnValidate()
    {
        //Debug.Log("OnValidate");
        #if UNITY_EDITOR
                recipeName = this.name;
                UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
