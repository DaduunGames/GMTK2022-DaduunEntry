using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultFloater : MonoBehaviour
{
    public int StoredValue = 1;

    private Image image;
    public Sprite[] numbers;

    bool DoneDisplaying = false;
    private Vector2 ScreenCenter;
    private Vector2 CornerLocation;

    public float AcceptableError = 1;
    public float FloatSpeed = 1;
    private bool Showing = true;
    private BulletSelection bs;

    private void Start()
    {
        image = GetComponent<Image>();
        ScreenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        CornerLocation = FindObjectOfType<FloaterDestination>().transform.position;
        transform.localScale = Vector3.zero;
        bs = FindObjectOfType<BulletSelection>();
    }

    private void FixedUpdate()
    {
        image.sprite = numbers[StoredValue - 1];

        if (Showing)
        {
            if (transform.localScale.x < 1)
            {
                transform.localScale += Vector3.one * Time.deltaTime * 2;
            }
        }
        else
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale -= Vector3.one * Time.deltaTime * 5;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if (DoneDisplaying == false)
        {
            if (Vector2.Distance(transform.position, ScreenCenter) > AcceptableError)
            {
                HeadTowards(ScreenCenter, FloatSpeed);
            }
            else
            {
                DoneDisplaying = true;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, CornerLocation) > AcceptableError)
            {
                HeadTowards(CornerLocation, FloatSpeed);
            }
            else
            {
                bs.ReloadBullet(StoredValue);
                Showing = false;
            }
        }
    }

    public void init(int storedValue)
    {
        StoredValue = storedValue;
    }

    private void HeadTowards(Vector2 target, float speed)
    {
        transform.position = Vector2.Lerp(transform.position, target, speed * Time.deltaTime);
    }
}
