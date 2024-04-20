using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiptBook : MonoBehaviour
{
    public Animator animator;

   public void SetInteractionRecieptBook()
    {
        animator.SetTrigger("Interact");
    }
}
