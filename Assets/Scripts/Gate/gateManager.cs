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
    
    protected virtual void OnEnable()
    {
        setColour();
        setValue();
    }

    private void colliderReset(Collider other)
    {
        transform.GetComponent<BoxCollider>().enabled = false;
        arrowController.Instance.disableCollider();
    }

    private void setColour()
    {
        _gateMesh.material = (_gateType == gateType.Minus || _gateType == gateType.Divide) 
            ? GameConfig.Instance.RedMat :  GameConfig.Instance.BlueMat;
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
            colliderReset(other);
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

            transform.DOPunchPosition(Vector3.up * .25f, .25f, 1, 1, false).OnComplete(() =>
            {
                transform.DOMove(new Vector3(transform.localPosition.x, -5f, transform.localPosition.z), 1f, false);
            });

        }
    }
}
