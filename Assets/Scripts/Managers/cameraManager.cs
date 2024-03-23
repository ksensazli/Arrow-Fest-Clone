using Cinemachine;
using DG.Tweening;
using Dreamteck.Splines;
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

    private void resetCamera()
    {
        _followerCamera.enabled = false;
        _celebrationCamera.enabled = true;
        DOVirtual.DelayedCall(1f, () =>
        {
            _celebrationCamera.enabled = false;
        });
    }
    
    private void OnLevelLoaded()
    {
        _followerCamera.transform.position = new Vector3(0f, 7f, -9f);
        _followerCamera.transform.rotation = Quaternion.Euler(17.354f, 0f, 0f);
        _followerCamera.enabled = true;
        _celebrationCamera.enabled = false;
    }

    private void OnLevelCompleted()
    {
        resetCamera();
    }
    
    private void OnLevelFailed()
    {
        resetCamera();
    }
}
