using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    public RecipeSO recipe1;
    public RecipeSO recipe2;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(recipe1.Equals(recipe2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
