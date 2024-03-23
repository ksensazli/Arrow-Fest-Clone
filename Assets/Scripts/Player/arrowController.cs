using System;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;

public class arrowController : MonoBehaviour
{
    public static Action<int> onArrowCountChanged;
    
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _arrowObject;

    [SerializeField] private float _slideSpeed = 0.01f;
    [SerializeField] private float _sideBounds = 2;
    [SerializeField] private float _lerpSpeed = 3;
    private CapsuleCollider _arrowCollider;
    private bool _isStart;
    private bool _isStopped;
    private bool _isEndLineReached;
    [HideInInspector] public SplineFollower _splineFollower;
    private inputManager _inputManager;
    public List<GameObject> arrowList = new List<GameObject>();
    public int arrowCount => arrowList.Count;
    public static arrowController Instance { get; private set; }

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("arrow Controller enable");
        }
        gameManager.onLevelStart += OnLevelStart;
        gameManager.onLevelLoaded += OnLevelLoaded;
        _arrowCollider = GetComponent<CapsuleCollider>();
        _inputManager = GetComponent<inputManager>();
        _splineFollower = GetComponentInParent<SplineFollower>();
        _splineFollower.followSpeed = 0;
    }

    private void OnDisable()
    {
        gameManager.onLevelStart -= OnLevelStart;
        gameManager.onLevelLoaded -= OnLevelLoaded;
        Debug.Log("arrow Controller disable");
    }

    private void getPooledObjects()
    {
        GameObject arrowClone = objectPool.Instance.GetPooledObject(0);
        arrowClone.SetActive(true);
        arrowClone.transform.parent = _arrowObject.transform.parent;
        arrowClone.transform.localPosition = Vector3.zero;
        arrowList.Add(arrowClone);
    }

    private void OnLevelLoaded()
    {
        _splineFollower.follow = false;
        _splineFollower.spline = levelManager.Instance.level.splineComputer;
        _splineFollower.Restart();
        arrowList.Clear();
        _arrowObject.gameObject.SetActive(true);
        arrowList.Add(_arrowObject);
        var additionalArrowAmount = PlayerPrefs.GetInt("Arrow");
        onArrowCountChanged?.Invoke(additionalArrowAmount + 1);
        transform.localPosition = Vector3.up;
        for (int i = 0; i < additionalArrowAmount; i++)
        {
            getPooledObjects();
            circleArrow(1);
        }
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
    }

    public void killEnemy(Transform enemy)
    {
        //TO DO
    }

    public void disableCollider()
    {
        _arrowCollider.enabled = false;
        DOVirtual.DelayedCall(.6f, () => _arrowCollider.enabled = true);
    }

    private void OnLevelStart()
    {
        _splineFollower.onEndReached -= OnEndReached;
        _isStopped = false;
        _isEndLineReached = false;
        _splineFollower.followSpeed = 8;
        _splineFollower.follow = true;
        _arrowCollider.enabled = true;
        _arrowCollider.radius = 0.10137f;
        _isStart = true;
        _splineFollower.onEndReached += OnEndReached;
    }

    private void failedGame()
    {
        _isStopped = true;
        _splineFollower.follow = false;
        if (_isEndLineReached)
        {
            return;
        }
        else
        {
            gameManager.Instance.failedLevel();
        }
    }

    private void finishLevel()
    {
        gameManager.Instance.completeLevel();
        _arrowCollider.enabled = false;
        DOTween.Kill(_player.transform);
        Debug.LogError("Finish Level");
        for (int i = 0; i < arrowCount; i++)
        {
            GameObject arrowClone = arrowList[^1 ];
            arrowList.RemoveAt(arrowList.Count - 1);
            Destroy(arrowClone);
        }
        GameConfig.Instance.levelNum++;
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
        int targetAmount = arrowCount / amount;
        int reduceAmount = arrowCount - targetAmount;
        arrowMinus(reduceAmount);
    }

    public void arrowSum(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            getPooledObjects();
        }

        onArrowCountChanged(amount);
        circleArrow(1);
    }

    public void arrowMinus(int amount)
    {
        var reduceAmount = Math.Min(amount, arrowCount);
        onArrowCountChanged(-reduceAmount);
        for (int i = 0; i < reduceAmount; i++)
        {
            if (arrowCount<=0)
            {
                break;
            }
            GameObject arrowClone = arrowList[^1 ];
            arrowList.RemoveAt(arrowList.Count - 1);
            if (arrowCount == 0)
            {
                _arrowObject.gameObject.SetActive(false);
            }
            else
            {
                objectPool.Instance.returnToPool(arrowClone);
            }
        }
        
        if (arrowCount <= 0 && !_isEndLineReached)
        {
            failedGame();
        }

        if (arrowCount <= 0 && _isEndLineReached)
        {
            for (int i = 0; i < GameConfig.Instance.winConfetties.Length; i++)
            {
                GameConfig.Instance.winConfetties[i].Play();
            }
            finishLevel();
        }
    }
    private Vector3 targetPosition = new Vector3(0,1,0);
    private void movePlayer()
    {
       

        if (Input.GetMouseButton(0))
        { 
            targetPosition = _player.transform.localPosition;
            targetPosition.x += _inputManager.DragAmountX * _slideSpeed;
             targetPosition.x = Mathf.Clamp(targetPosition.x, -_sideBounds, _sideBounds);
        }
        Debug.LogError(_inputManager.DragAmountX * _slideSpeed);
        Vector3 targetPositionLerp = new Vector3(Mathf.MoveTowards(_player.localPosition.x, targetPosition.x, Time.deltaTime * _lerpSpeed),
            Mathf.MoveTowards(_player.localPosition.y, targetPosition.y, Time.deltaTime * _lerpSpeed),
            Mathf.MoveTowards(_player.localPosition.z, targetPosition.z, Time.deltaTime * _lerpSpeed));

        _player.localPosition = targetPositionLerp;
    }

    private void circleArrow(int forEndVertical)
    {
        arrowList[0].transform.localPosition = Vector3.zero;
        int arrowIndex = 1;
        int circleOrder = 1;
        
        while (true)
        {
            float radius = circleOrder * .1f;
            _arrowCollider.radius = 0.14f * circleOrder;
            for (int i = 0; i < (circleOrder + 1) * 4; i++)
            {
                if (arrowIndex == arrowCount)
                {
                    return;
                }

                float radians = 2 * Mathf.PI / (circleOrder + 1) / 4 * i;
                float vertical = Mathf.Sin(radians);
                float horizontal = Mathf.Cos(radians);

                Vector3 dir = new Vector3(horizontal, vertical / forEndVertical, 0f);
                Vector3 newPosition = dir * radius;

                GameObject _arrow = arrowList[arrowIndex];

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

    private void OnEndReached(double obj)
    {
        _player.DOLocalMoveX(0,.15f,false);
        _isStopped = true;
        _isEndLineReached = true;
        _arrowCollider.radius = 1;
        circleArrow(4);
        _player.DOMoveZ(165, 3f, false);
    }
}
