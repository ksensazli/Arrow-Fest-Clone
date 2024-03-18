using Cinemachine;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _followerCamera;
    [SerializeField] private CinemachineVirtualCamera _celebrationCamera;
    private void OnEnable()
    {
        gameManager.onLevelLoaded += OnLevelLoaded;
        gameManager.onLevelCompleted += OnLevelCompleted;
        gameManager.onLevelFailed += OnLevelFailed;
    }

    private void OnDisable()
    {
        gameManager.onLevelLoaded -= OnLevelLoaded;
        gameManager.onLevelCompleted -= OnLevelCompleted;
        gameManager.onLevelFailed -= OnLevelFailed;
    }
    
    private void OnLevelLoaded()
    {
        _followerCamera.transform.position = new Vector3(0f, 7f, -9f);
        _followerCamera.transform.rotation = Quaternion.Euler(17.354f, 0f, 0f);
        _followerCamera.enabled = true;
    }

    private void OnLevelCompleted()
    {
        _followerCamera.enabled = false;
    }
    
    private void OnLevelFailed()
    {
        _followerCamera.enabled = false;
    }
}
