using UnityEngine;
using UnityEngine.UI;

public class incomeEnhancer : MonoBehaviour
{
    [SerializeField] private Button _enhanceButton;
    private int _goldAmount;
    private float _costIncome;
    private int _incomeLevel;

    private void OnEnable()
    {
        _enhanceButton.onClick.AddListener( () => OnEnhanceButton());
        _goldAmount = PlayerPrefs.GetInt("Gold");
        _incomeLevel = PlayerPrefs.GetInt("Income", 1);
        _costIncome = 50 * (PlayerPrefs.GetInt("Level") * 0.1f + 1);
    }

    private void OnDisable()
    {
        _enhanceButton.onClick.RemoveAllListeners();
    }

    private void OnEnhanceButton()
    {
        if (_goldAmount > 50)
        {
            for (int i = 0; i < _costIncome; i++)
            {
                _goldAmount--;
            }
            _incomeLevel += 1;
            PlayerPrefs.SetInt("Income", _incomeLevel);
            PlayerPrefs.SetInt("Gold", _goldAmount);
            Debug.Log("Gold amount is: " + _goldAmount);
        }
        else
        {
            Debug.Log("Not enough gold for this upgrade");
        }
        
    }
}
