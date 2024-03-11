using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    public static levelManager Instance { get; private set; }
    private GameObject level;

    public int levelCount;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        initLevel();
    }

    public void clearLevel()
    {
        Destroy(level);
    }

    public void initLevel()
    {
        level = Instantiate(GameConfig.Instance.Levels[0], transform);
    }

    public void nextLevel()
    {
        level = Instantiate(GameConfig.Instance.Levels[1], transform);
    }
}
