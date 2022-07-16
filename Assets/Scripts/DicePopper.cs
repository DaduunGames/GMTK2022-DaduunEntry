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
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Die.transform.Rotate(new Vector3(0, 0, 1), Rotation * Time.deltaTime * 10);
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
        IsPopping = true;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("PressDown"))
        {
            anim.SetTrigger("StartPop");
        }

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


        FindObjectOfType<BulletSelection>().SelectNewBullet(DiceValue);
    }
}
