using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "ScriptableObject/Ingredient", order = 2)]
public class IngredientSO : ScriptableObject
{
    public enum IngredientType
    {
        TOPPING,
        TONIC,
        OTHERS
    }
    [Header("General")]
    [SerializeField] public string ingredientName;
    public int ingredientID;
    [SerializeField] public IngredientType ingredientType;
    [SerializeField] public bool isAlcoholic;

    // IDK...The Assets???

    public override string ToString()
    {
        return $"Ingredient: {ingredientName}, Type: {ingredientType}, Alcoholic: {isAlcoholic}";
    }

}
