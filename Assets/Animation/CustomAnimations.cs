using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAnimations : MonoBehaviour
{
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int IsCheering = Animator.StringToHash("IsCheering");
    private static readonly int Speed = Animator.StringToHash("Speed");

    private Animator _animator;
    private bool _flag = false;
    private bool _cheering = false;
    private float _speed = 0;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _flag = !_flag;

            _animator.SetBool(IsDead, _flag);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            _cheering = !_cheering;

            _animator.SetBool(IsCheering, _cheering);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _animator.SetTrigger(Hit);
        }

        if (Input.GetKey(KeyCode.W))
        {
            _speed = Mathf.Lerp(_speed, 1, Time.deltaTime);
            _animator.SetFloat(Speed, _speed);
        }
        else
        {
            _speed = Mathf.Lerp(_speed, 0, Time.deltaTime);
            _animator.SetFloat(Speed, _speed);
        }
    }
}