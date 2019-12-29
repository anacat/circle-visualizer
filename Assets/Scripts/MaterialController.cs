using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialController : MonoBehaviour
{
    private Material _material;
    private float _time;

    private void Awake()
    {
        _material = GetComponent<Image>().material;
    }

    private void Update()
    {
        float t = Mathf.Min(Mathf.PingPong(Time.time, _time) / _time, 1f);
        _material.SetFloat("_MyTime", t);
    }

    public void SetVerticesValue(float value)
    {
        _material.SetFloat("_Points", value);
    }

    public void SetProductValue(float value)
    {
        _material.SetFloat("_Product", value);
    }

    public void SetColorValue(Color color)
    {
        _material.SetColor("_Color", color);
    }

    public void SetTimeValue(float value)
    {
        _time = value;
    }

    public void ShowCircle(bool enable)
    {
        _material.SetInt("_ShowCircle", enable ? 1 : 0);
    }
}
