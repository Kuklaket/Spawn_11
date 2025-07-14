using System;
using System.Collections;
using UnityEngine;

public class EnemyCat : MonoBehaviour
{
    private Vector2 _vectorMove;   
    private int _speed = 1;
    private Coroutine _liveCoroutine;

    public event Action<EnemyCat> ReadyForReleased;

    private void OnEnable()
    {
        _liveCoroutine = StartCoroutine(StartLive());
    }

    private void OnDisable()
    {
        if (_liveCoroutine != null)
        {
            StopCoroutine(_liveCoroutine);
            _liveCoroutine = null;
        }
    }

    private void Update()
    {
        transform.Translate(_vectorMove.normalized * _speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 direction)
    {
        _vectorMove = direction;
    }

    private IEnumerator StartLive()
    {
        int liveLenght = 5;

        yield return new WaitForSeconds(liveLenght);
        ReadyForReleased?.Invoke(this);
    }
}