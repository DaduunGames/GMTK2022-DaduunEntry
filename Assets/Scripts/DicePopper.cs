using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DicePopper : MonoBehaviour
{
    public int DiceValue = 1;
    public bool IsPopping = false;

    public Image Die;
    public Sprite IcoDieOdds;
    public Sprite IcoDieEvens;

    private Animator anim;

    public float Rotation = 0;

    private bool firstReloadDone = false;

    public GameObject DiceFloaterPrefab;
    public Transform MainCanvas;

    public AudioSource SOURCE;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Die.transform.Rotate(new Vector3(0, 0, 1), Rotation * Time.deltaTime * 10);

        if (Input.GetKeyDown(KeyCode.R))
        {
            PressDown();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            StartPop();
        }
    }

    public void PressDown()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.SetTrigger("Down");
        }


    }

    public void StartPop()
    {
        SOURCE.Play();

        IsPopping = true;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("PressDown"))
        {
            anim.SetTrigger("StartPop");
        }
        BulletSelection.instance.ReloadPrompt.SetActive(false);

    }

    public void FinishPop()
    {
        Debug.Log("finished pop");


        IsPopping = false;
        DiceValue = Random.Range(1, 7);

        switch (DiceValue)
        {
            default:
            case 1:
                Die.sprite = IcoDieOdds;
                Die.transform.rotation = Quaternion.Euler(0, 0, 240);
                
                break;
            case 2:
                Die.sprite = IcoDieEvens;
                Die.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 3:
                Die.sprite = IcoDieOdds;
                Die.transform.rotation = Quaternion.Euler(0, 0, 120);
                break;
            case 4:
                Die.sprite = IcoDieEvens;
                Die.transform.rotation = Quaternion.Euler(0, 0, 240);
                break;
            case 5:
                Die.sprite = IcoDieOdds;
                Die.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 6:
                Die.sprite = IcoDieEvens;
                Die.transform.rotation = Quaternion.Euler(0, 0, 120);
                break;
        }

        if (firstReloadDone)
        {
            //bSelect.ReloadBullet(DiceValue);
            ResultFloater floater = Instantiate(DiceFloaterPrefab, Die.transform.position, Quaternion.identity, MainCanvas).GetComponent<ResultFloater>();
            floater.init(DiceValue);
        }
        else
        {
            firstReloadDone = true;
        }
       
    }
}
