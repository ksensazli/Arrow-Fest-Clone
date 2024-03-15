using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    public static levelManager Instance { get; private set; }
    public Level level;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [Button]
    private void destroyLevel()
    {
        if (level != null)
        {
            Destroy(level.gameObject);
        }
    }

    [Button]
    public void initLevel()
    {
        // StartCoroutine(loadLevel());
        if (level != null)
        {
            Destroy(level.gameObject);
        }
        PlayerPrefs.SetInt("Level", GameConfig.Instance.levelNum);
        level = Instantiate(GameConfig.Instance.Levels[(PlayerPrefs.GetInt("Level"))], transform);
    }

    // private IEnumerator loadLevel()
    // {
    //     if (level != null)
    //     {
    //         Destroy(level);
    //     }
    //     yield return new WaitForEndOfFrame();
    //     PlayerPrefs.SetInt("Level", GameConfig.Instance.levelNum);
    //     level = Instantiate(GameConfig.Instance.Levels[(PlayerPrefs.GetInt("Level"))], transform);
    // }
}
