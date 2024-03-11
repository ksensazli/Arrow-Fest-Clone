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
    
    [Button]
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
