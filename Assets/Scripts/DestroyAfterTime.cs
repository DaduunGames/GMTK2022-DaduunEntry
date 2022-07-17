using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float Time = 3;
    public bool IgnoreBulletScript = false;
    void Start()
    {
        StartCoroutine(KillObj());
    }

    IEnumerator KillObj()
    {
        yield return new WaitForSeconds(Time);

        if (GetComponent<Bullet>() && IgnoreBulletScript == false)
        {
            GetComponent<Bullet>().Hit();
        }
        else
        {
            Destroy(gameObject);
        }

       
    }

}
