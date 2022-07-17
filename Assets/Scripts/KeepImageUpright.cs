using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepImageUpright : MonoBehaviour
{
    public SpriteRenderer sp;

    void Start()
    {
        if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270)
        {
            sp.flipY = true;
        }
        else
        {
            sp.flipY = false;
        }
    }
}
