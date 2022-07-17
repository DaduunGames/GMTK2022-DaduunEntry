using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class InteractZone : MonoBehaviour
{

    public GameObject Prompt;
    public float InteractDistance = 3;
    public bool ShowDistanceGizmo = true;
    public bool DestroyAfterInteract = false;
    public bool RefreshAStarAfterInteract = true;
    

    public GameObject Player;
    public Interactable[] interactables;

    void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) <= InteractDistance)
        {
            Prompt.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                foreach(Interactable i in interactables)
                {
                    i.Interact();
                }

                if (DestroyAfterInteract)
                {
                    Destroy(gameObject);
                }

                if (RefreshAStarAfterInteract)
                {
                    AstarPath.active.Scan();
                }
            }
        }
        else
        {
            Prompt.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        if (ShowDistanceGizmo)
        {
            Gizmos.DrawWireSphere(transform.position, InteractDistance);
        }
    }
}
