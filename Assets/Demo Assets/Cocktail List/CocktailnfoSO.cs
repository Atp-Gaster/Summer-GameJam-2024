using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CocktailInfoSO", menuName = "ScriptableObject/CocktailInfoSO", order = 1)]
public class CocktailInfoSO : ScriptableObject
{
    [field: Header("Genaral")]
    [field: SerializeField] public string Name { get; private set; }    
    [field: SerializeField] public bool IsOnIce { get; private set; }
    [field: SerializeField] public bool IsHaveLime { get; private set; }
    [field: SerializeField] public bool IsHaveBoba { get; private set; }

    [field: Header("Ingrediant Setting")]
    [field: SerializeField] public string[] Ingrediant { get; private set; }
    [field: SerializeField] public int[] NumberOfIngrediant { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }

}
