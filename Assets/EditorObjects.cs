using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EditorObjects : MonoBehaviour
{
    public GameObject[] EnableOnPlay;
    public GameObject[] disableOnPlay;
    private ShadowCaster2D[] DisableRenderers;

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
        DisableRenderers = FindObjectsOfType<ShadowCaster2D>();
        foreach (ShadowCaster2D ShadowCast in DisableRenderers)
        {
            ShadowCast.GetComponent<Renderer>().enabled = false;
        }
    }
}
