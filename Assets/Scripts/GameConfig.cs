using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public static GameConfig Instance { get; private set; }
    [Title("Gates Material")]
    public Material RedMat;
    public Material BlueMat;

    [Title("Particles")] 
    public ParticleSystem _winConfetties;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
