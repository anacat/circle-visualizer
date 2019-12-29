using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public MaterialController materialController;

    public CanvasGroup canvasGroup;

    [Header("UI")]
    public InputField vertexNumbers;
    public Slider vertexSlider;
    public InputField productNumber;
    public Slider productSlider;
    public InputField timeNumber;
    public Slider timeSlider;

    private bool _isShowing = true;

    private void Start()
    {
        vertexNumbers.text = vertexSlider.value.ToString();
        productNumber.text = productSlider.value.ToString();
        timeNumber.text = timeSlider.value.ToString();

        materialController.SetVerticesValue(vertexSlider.value);
        materialController.SetProductValue(productSlider.value);
        materialController.SetTimeValue(timeSlider.value);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            _isShowing = !_isShowing;

            canvasGroup.alpha = _isShowing ? 1 : 0;
            canvasGroup.interactable = _isShowing;
            canvasGroup.blocksRaycasts = _isShowing;
        }
    }

    public void VertexSliderChange()
    {
        vertexNumbers.text = vertexSlider.value.ToString();
        materialController.SetVerticesValue(vertexSlider.value);
    }

    public void ProductSliderChange()
    {
        productNumber.text = productSlider.value.ToString();
        materialController.SetProductValue(productSlider.value);
    }

    public void TimeSliderChange()
    {
        timeNumber.text = timeSlider.value.ToString();
        materialController.SetTimeValue(timeSlider.value);
    }

    public void ShowCircleChange(bool enable)
    {
        materialController.ShowCircle(enable);
    }

    public void ColorChange(Color color)
    {
        materialController.SetColorValue(color);
    }

    public void SetVertices(string value)
    {
        materialController.SetVerticesValue(float.Parse(value));
    }

    public void SetProduct(string value)
    {
        materialController.SetProductValue(float.Parse(value));
    }

    public void SetTime(string value)
    {
        materialController.SetTimeValue(float.Parse(value));
    }
}
