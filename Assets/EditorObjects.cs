using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorObjects : MonoBehaviour
{
    public GameObject[] EnableOnPlay;
    public GameObject[] disableOnPlay;
    public Renderer[] DisableRenderers;

    private void Start()
    {
        foreach (GameObject obj in EnableOnPlay)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in disableOnPlay)
        {
            obj.SetActive(false);
        }
        foreach (Renderer rend in DisableRenderers)
        {
            rend.enabled = false;
        }
    }
}
