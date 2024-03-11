using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    public static levelManager Instance { get; private set; }
    private GameObject level;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        initLevel();
    }

    private void initLevel()
    {
        level = Instantiate(GameConfig.Instance.Levels[0], transform);
    }
}
