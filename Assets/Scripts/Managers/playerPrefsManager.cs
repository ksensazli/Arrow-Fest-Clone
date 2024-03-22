using Sirenix.OdinInspector;
using UnityEngine;

public class playerPrefsManager : MonoBehaviour
{
    [ShowInInspector]
    public int GoldAmount
    {
        get
        {
            return PlayerPrefs.GetInt("Gold",0);
        }
        set
        {
            PlayerPrefs.SetInt("Gold", value);
            gold.OnGoldCollected?.Invoke(value);
        }
    }
    
    [ShowInInspector]
    public int InitialArrow
    {
        get
        {
            return PlayerPrefs.GetInt("Arrow");
        }
        set
        {
            PlayerPrefs.SetInt("Arrow", value);
            arrowController.onArrowCountChanged?.Invoke(value);
        }
    }
    
    [ShowInInspector]
    public int IncomeLevel
    {
        get
        {
            return PlayerPrefs.GetInt("Income");
        }
        set
        {
            PlayerPrefs.SetInt("Income", value);
        }
    }

    [ShowInInspector]
    public int HighScore
    {
        get
        {
            return PlayerPrefs.GetInt("Level");
        }
        set
        {
            PlayerPrefs.SetInt("Level", value);
        }
    }
    
    [Button]
    public void Clear()
    {
        PlayerPrefs.DeleteAll();
    }
}
