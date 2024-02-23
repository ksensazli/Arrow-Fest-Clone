using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum gateType { Sum = 0, Minus = 1, Multiply = 2, Divide = 3 }
public class gateManager : MonoBehaviour
{
    [SerializeField] private gateType _gateType;
    [SerializeField] private int _gateValue;
    [SerializeField] private TMP_Text _gateText;
    [SerializeField] private MeshRenderer gateRend;
    [SerializeField] private Material _blueMaterial;
    [SerializeField] private Material _redMaterial;


    private void Awake()
    {
        UpdateColor();
        UpdateText();
    }

    private void UpdateColor()
    {
        gateRend.material = (_gateType == gateType.Minus || _gateType == gateType.Divide) ? _redMaterial : _blueMaterial;
    }
    
    private void UpdateText()
    {
        if (_gateType == gateType.Sum)
            _gateText.text = $"+{_gateValue}";
        else if (_gateType == gateType.Multiply)
            _gateText.text = $"x{_gateValue}";
        else if (_gateType == gateType.Minus)
            _gateText.text = $"-{_gateValue}";
        else
            _gateText.text = $"÷{_gateValue}";
    }
}
