using DG.Tweening;
using TMPro;
using UnityEngine;

public enum gateType { Sum = 0, Minus = 1, Multiply = 2, Divide = 3 }
public class gateManager : MonoBehaviour
{
    [SerializeField] private gateType _gateType;
    [SerializeField] private int _gateValue;
    [SerializeField] private TMP_Text _gateText;
    [SerializeField] private MeshRenderer _gateMesh;
    [SerializeField] private Material _blueMaterial;
    [SerializeField] private Material _redMaterial;
    
    private void OnEnable()
    {
        updateColor();
        updateText();
    }

    private void updateColor()
    {
        _gateMesh.material = (_gateType == gateType.Minus || _gateType == gateType.Divide) ? _redMaterial : _blueMaterial;
    }
    
    private void updateText()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("arrow"))
        {
            Debug.Log("carpisma");
        }
    }
}
