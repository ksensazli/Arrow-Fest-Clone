using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class canvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _startScreen;
    [SerializeField] private GameObject _infoScreen;
    [SerializeField] private GameObject _failedScreen;
    [SerializeField] private TMPro.TMP_Text _arrowCount;
    [SerializeField] private TMPro.TMP_Text _levelCount;
    private int _arrowData;

    private void OnEnable()
    {
        gameManager.onLevelStart += startScreen;
        gameManager.onLevelFailed += failedScreen;
        _startScreen.SetActive(true);
        _levelCount.text = "LEVEL " + (SceneManager.GetActiveScene().buildIndex + 1).ToString();
    }

    private void OnDisable()
    {
        gameManager.onLevelStart -= startScreen;
        gameManager.onLevelFailed -= failedScreen;
    }
    
    private void Update()
    {
        //_arrowData = arrowController.Instance.arrowCount + 1;
        _arrowCount.text = arrowController.Instance.arrowCount.ToString();
    }

    private void startScreen()
    {
        DOVirtual.DelayedCall(.1f, () => _startScreen.SetActive(false)).OnComplete(() =>
        {
            _infoScreen.SetActive(true);
        });
    }

    private void failedScreen()
    {
        _infoScreen.SetActive(false);
        _failedScreen.SetActive(true);
    }
}
