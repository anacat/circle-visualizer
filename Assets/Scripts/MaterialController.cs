using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialController : MonoBehaviour
{
    public float time;
    public int maxPoints;
    public int maxProduct;

    private Material _material;

    private void Awake()
    {
        _material = GetComponent<Image>().material;
    }
    
    private void Update()
    {
        float t = Mathf.Min((Mathf.PingPong(Time.time, 0.9f) / 0.9f), 1f);
        _material.SetFloat("_MyTime", t);
    }
}
