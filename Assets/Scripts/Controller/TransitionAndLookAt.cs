using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum TransitionState
{
    NONE,
    TO,
    BACK
}

public class TransitionAndLookAt : MonoBehaviour
{
    private Vector3 _orignalPosition;
    private Quaternion _orignalRotation;
    private Vector3 _targetPosition;
    private Quaternion _targetRotation;

    private TransitionState _state = TransitionState.NONE; 

    private void Awake()
    {
        _orignalPosition = transform.position;
        _orignalRotation = transform.rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void transitionTo(Vector3 targetPostion, Quaternion targetRotation)
    {
        _targetPosition = targetPostion;
        _targetRotation = targetRotation;
        _state = TransitionState.TO;
    }

    public void transitionBack()
    {
        transitionTo(_orignalPosition, _orignalRotation);
    }

    // Update is called once per frame
    void Update()
    {

        if (_state == TransitionState.TO)
        {
            //正在转换的动画中
            Vector3 direciton = Vector3.Normalize(_targetPosition - transform.position);
            float distance = Vector3.Distance(transform.position, _targetPosition);

            if (distance < 0.1f)
            {
                transform.position = _targetPosition;
                _state = TransitionState.NONE;
            }
            else
            {
                transform.position += direciton * Time.deltaTime * 10f;
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, 2f * Time.deltaTime);

            }

        }
    }
}
