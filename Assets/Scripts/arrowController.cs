using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.SceneManagement;

public class arrowController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _arrowObject;
    [SerializeField] private float _slideSpeed;
    [SerializeField] private float _sideBounds;
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private TMPro.TMP_Text _goldText;
    private int _collectedGold;
    private bool _isStart;
    private bool _isStopped;
    private SplineFollower _splineFollower;
    private Vector3 _forwardMoveAmount;
    private inputManager _inputManager;
    private List<GameObject> _arrowList = new List<GameObject>();
    public int arrowCount => _arrowList.Count + 1;
    
    public static arrowController Instance { get; private set; }

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        gameManager.onLevelStart += startGame;
        gameManager.onLevelFailed += failedGame;
        _inputManager = GetComponent<inputManager>();
        _splineFollower = GetComponentInParent<SplineFollower>();
        _splineFollower.followSpeed = 0;
        _collectedGold = 0;
        _goldText.text = _collectedGold.ToString();
    }

    private void OnDisable()
    {
        gameManager.onLevelStart -= startGame;
        gameManager.onLevelFailed -= failedGame;
    }

    private void Update()
    {
        if (!_isStart)
        {
            return;
        }

        if (_isStopped)
        {
            return;
        }
        
        movePlayer();

        if (arrowCount < 0)
        {
            failedGame();
        }
    }

    private void startGame()
    {
        _splineFollower.followSpeed = 8;
        _isStart = true;
    }

    private void failedGame()
    {
        _isStopped = true;
        _splineFollower.follow = false;
    }

    public void arrowMultiply(int amount)
    {
        if (amount < 2)
            return;
        
        arrowSum((arrowCount) * (amount - 1));
    }

    public void arrowDivide(int amount)
    {
        if (amount < 1)
            return;
        
        float reduceAmount = (arrowCount) * (amount - 1) / (float)amount;

        arrowMinus(Mathf.CeilToInt(reduceAmount));
    }

    public void arrowSum(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject arrowClone = Instantiate(_arrowObject, transform);
            _arrowList.Add(arrowClone);
        }
        
        circleArrow();
    }

    public void arrowMinus(int amount)
    {
        if (amount > arrowCount)
        {
            failedGame();
            return;
        }
        
        for (int i = 0; i < amount -1; i++)
        {
            GameObject arrowClone = _arrowList[0];
            _arrowList.RemoveAt(0);
            Destroy(arrowClone);
        }
        _arrowList.RemoveAt(0);
        
        circleArrow();
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

    private void circleArrow()
    {
        _arrowList[0].transform.localPosition = Vector3.zero;
        int arrowIndex = 1;
        int circleOrder = 1;
        
        while (true)
        {
            float radius = circleOrder * .1f;

            for (int i = 0; i < (circleOrder + 1) * 4; i++)
            {
                if (arrowIndex == arrowCount)
                {
                    return;
                }

                float radians = 2 * Mathf.PI / (circleOrder + 1) / 4 * i;
                float vertical = Mathf.Sin(radians);
                float horizontal = Mathf.Cos(radians);

                Vector3 dir = new Vector3(horizontal, vertical, 0f);
                Vector3 newPosition = dir * radius;

                GameObject _arrow = _arrowList[arrowIndex];

                if (_arrow != null)
                {
                    _arrow.transform.DOKill();
                    _arrow.transform.DOLocalMove(newPosition, 0.25f);
                }
                arrowIndex++;
            }
            circleOrder++;
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
