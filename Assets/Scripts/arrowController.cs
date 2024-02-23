using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;

public class arrowController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _slideSpeed;
    [SerializeField] private float _sideBounds;
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private TMPro.TMP_Text _goldText;
    private int _collectedGold;
    private Vector3 _forwardMoveAmount;
    private inputManager _inputManager;

    private void OnEnable()
    {
        _inputManager = GetComponent<inputManager>();
        _collectedGold = 0;
        _goldText.text = _collectedGold.ToString();
    }

    private void Update()
    {
        movePlayer();
    }
    
    private void movePlayer()
    {
        Vector3 targetPosition = _player.transform.localPosition;

        if (Input.GetMouseButton(0))
        {
            targetPosition.x = 0;
            targetPosition.x = _inputManager.DragAmountX * _slideSpeed;
            targetPosition.x = Mathf.Clamp(targetPosition.x, -_sideBounds, _sideBounds);

            Vector3 targetPositionLerp = new Vector3(Mathf.Lerp(_player.localPosition.x, targetPosition.x, Time.fixedDeltaTime * _lerpSpeed),
                Mathf.Lerp(_player.localPosition.y, targetPosition.y, Time.fixedDeltaTime * _lerpSpeed),
                Mathf.Lerp(_player.localPosition.z, targetPosition.z, Time.fixedDeltaTime * _lerpSpeed));

            _player.localPosition = targetPositionLerp;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("gold"))
        {
            Debug.Log("Triggered gold");
            //other.transform.DOMove(_goldText.transform.position, 60f, false);
            other.transform.DOScale(0f, 0.2f).OnComplete(() => other.gameObject.SetActive(false));

            _collectedGold++;
            _goldText.text = _collectedGold.ToString();
        }
    }
}
