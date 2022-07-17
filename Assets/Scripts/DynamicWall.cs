using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicWall : Interactable { 
    public bool CanBeDestroyedWithViolence = false;
    public void DestroyWithViolence()
    {
        if (CanBeDestroyedWithViolence == true)
        {
            Destroy(gameObject);
        }
    }

    public override void Interact()
    {
        Destroy(gameObject);
    }

}
