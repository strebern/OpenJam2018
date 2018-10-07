using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMouseScript : MonoBehaviour
{


    [SerializeField] private float spawnLerpDelay;

    public TaskManagerScript taskManagerScript;

    private Transform _lerpTarget1;
    private Transform _lerpTarget2;
    private Transform _focusTarget;

    private bool _firstTargetSelected = false;
    private bool _canLerp = false;

    private void Awake()
    {
        _lerpTarget1 = taskManagerScript.SelectProcessTarget;
        _lerpTarget2 = taskManagerScript.SelectDeleteTarget;
        _focusTarget = _lerpTarget1;

        StartCoroutine(InstantiationLerpDelay());
    }

    private void Update()
    {
        taskManagerScript.IsTargetObstructed(_focusTarget);
        ChooseTarget();
        if (_canLerp)
            LerpToTarget();
    }

    private void ChooseTarget()
    {
        if (!_firstTargetSelected)
            _focusTarget = _lerpTarget1;
        else
            _focusTarget = _lerpTarget2;

        if (transform.position == _lerpTarget1.position)
            _firstTargetSelected = true;

    }

    private void LerpToTarget()
    {
        transform.position = new Vector3
            (Mathf.Lerp(transform.position.x, _focusTarget.position.x, 0.05f),
            Mathf.Lerp(transform.position.y, _focusTarget.position.y, 0.05f),
            0);
    }

    IEnumerator InstantiationLerpDelay()
    {
        yield return new WaitForSeconds(spawnLerpDelay);
        _canLerp = true;
    }
}
