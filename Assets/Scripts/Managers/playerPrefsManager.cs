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
    public int HighScore
    {
        get
        {
            return PlayerPrefs.GetInt("Level");
        }
    }
    
    [Button]
    public void Clear()
    {
        PlayerPrefs.DeleteAll();
    }
}
