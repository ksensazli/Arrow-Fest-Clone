using System;
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
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        initLevel();
    }

    public void initLevel()
    {
        if (level != null)
        {
            Destroy(level);
        }
        level = Instantiate(GameConfig.Instance.Levels[(GameConfig.Instance.levelNum)], transform);
    }
}
