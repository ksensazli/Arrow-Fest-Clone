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
    [SerializeField] private bool _isDynamic;
    private float _durationTime;
    
    private void OnEnable()
    {
        setColour();
        setValue();
        
        _durationTime = Random.Range(1f, 3f);
        if (_isDynamic)
        {
            if (transform.localPosition.x > 0)
            {
                transform.DOLocalMoveX(-1f, _durationTime).SetLoops(-1, LoopType.Yoyo);
            }
            else
            {
                transform.DOLocalMoveX(1f, _durationTime).SetLoops(-1, LoopType.Yoyo);
            }
        }
    }

    private void colliderReset()
    {
        transform.GetComponent<BoxCollider>().enabled = false;
        GameObject arrow = GameObject.FindWithTag("arrow");
        arrow.transform.GetComponent<CapsuleCollider>().enabled = false;
        DOVirtual.DelayedCall(.6f, () => arrow.transform.GetComponent<CapsuleCollider>().enabled = true);
    }

    private void setColour()
    {
        _gateMesh.material = (_gateType == gateType.Minus || _gateType == gateType.Divide) ? _redMaterial : _blueMaterial;
    }
    
    private void setValue()
    {
        if (_gateType == gateType.Sum)
        {
            _gateText.text = $"+{_gateValue}";
        }
        else if (_gateType == gateType.Multiply)
        {
            _gateText.text = $"x{_gateValue}";
        }
        else if (_gateType == gateType.Minus)
        {
            _gateText.text = $"-{_gateValue}";
        }
        else
        {
            _gateText.text = $"÷{_gateValue}";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("arrow"))
        {
            Debug.Log("Passed gate is: " + _gateText.text);
            colliderReset();
            transform.DOKill();

            switch (_gateType)
            {
                case gateType.Sum:
                    arrowController.Instance.arrowSum(_gateValue);
                    break;
                case gateType.Minus:
                    arrowController.Instance.arrowMinus(_gateValue);
                    break;
                case gateType.Multiply:
                    arrowController.Instance.arrowMultiply(_gateValue);
                    break;
                case gateType.Divide:
                    arrowController.Instance.arrowDivide(_gateValue);
                    break;
            }
        }
    }
}
