using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float Speed = 1;

    private Coroutine _liveCoroutine;
    private Transform _target;

    public event Action<Enemy> ReadyForReleased;

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
        Vector2 direction = (_target.position - transform.position).normalized;

        transform.Translate(direction * Speed * Time.deltaTime);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private IEnumerator StartLive()
    {
        int liveLenght = 3;

        yield return new WaitForSeconds(liveLenght);
        ReadyForReleased?.Invoke(this);
    }
}