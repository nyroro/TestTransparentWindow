using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCam;
    private NavMeshAgent _agent;
    private NavMeshPath _path;

    private Animator _anim;
    private InputAction action;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _path = new NavMeshPath();
        _anim = GetComponentInChildren<Animator>();
        action = new InputAction(binding:"<Mouse>/leftButton");
        action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (action.triggered && _agent.isOnNavMesh)
        {
            var mousePos = Mouse.current.position.ReadValue();
            Debug.Log($"mouse pos {mousePos}");
            var targetPos = _mainCam.ScreenToWorldPoint(mousePos);
            targetPos = new Vector3(targetPos.x, targetPos.y, transform.position.z);
            _agent.CalculatePath(targetPos, _path);
            if (_path.status == NavMeshPathStatus.PathComplete)
            {
                _agent.SetPath(_path);
            }
        }

        _anim.SetBool("walking", _agent.velocity.magnitude > 0.1f);
        if (_agent.velocity.magnitude > 0.1f)
        {
            transform.localScale = new Vector3(_agent.velocity.x > 0 ? 1 : -1, 1, 1);
        }
    }
}
