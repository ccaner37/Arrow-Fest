using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsScaleController : MonoBehaviour
{
    private Vector3 _initialScale;
    private void Start()
    {
        _initialScale = transform.localScale;
    }

    void Update()
    {
        float distanceToCenter = Vector3.Distance(Vector3.zero, transform.localPosition);
        float x = _initialScale.x + 1f * -distanceToCenter - 0.15f;
        transform.localScale = new Vector3(x, 1f, 1f);
    }
}
