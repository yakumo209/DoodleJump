using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adjust : MonoBehaviour
{
    private void Start()
    {
        Resize();
    }

    void Resize()
    {
        float width = GetComponent<SpriteRenderer>().bounds.size.x;
        float targetWidth = Camera.main.orthographicSize * 2 / Screen.height * Screen.width;
        Vector3 scale = transform.localScale;
        scale.x = targetWidth / width;
        transform.localScale = scale;
    }
}
