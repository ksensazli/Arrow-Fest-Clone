using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class GameConfig : MonoBehaviour
{
    public static GameConfig Instance { get; private set; }
    [Title("Gates Material")]
    public Material RedMat;
    public Material BlueMat;
    
    [Title("Levels")] 
    public Level[] Levels;
    [HideInInspector] public int levelNum;

    [Title("Particles")] 
    public ParticleSystem[] winConfetties;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        levelNum = PlayerPrefs.GetInt("Level");
    }
}
