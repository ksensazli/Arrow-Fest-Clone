using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arrowBuyButton : MonoBehaviour
{
    [SerializeField] private Button _buyButton;
    private int _goldAmount;
    private int _arrowAmount;
    private float _costArrow;
    private void OnEnable()
    {
        _buyButton.onClick.AddListener(()=>OnBuyButton());
        _goldAmount = PlayerPrefs.GetInt("Gold");
        _arrowAmount = PlayerPrefs.GetInt("Arrow");
        _costArrow = 50 * (PlayerPrefs.GetInt("Level") * 0.1f + 1);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveAllListeners();
    }
    private void OnBuyButton()
    {
        if (_goldAmount > 50)
        {
            for (int i = 0; i < _costArrow; i++)
            {
                _goldAmount--;
            }
            _arrowAmount++;
            PlayerPrefs.SetInt("Arrow",_arrowAmount);
            PlayerPrefs.SetInt("Gold", _goldAmount);
            Debug.Log("Gold amount is: " + _goldAmount);
        }
        else
        {
            Debug.Log("Not enough gold for this upgrade");
        }
    }
}
