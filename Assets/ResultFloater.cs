using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultFloater : MonoBehaviour
{
    public int StoredValue = 1;

    private Image image;
    public Sprite[] numbers;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        image.sprite = numbers[StoredValue - 1];
    }
}
