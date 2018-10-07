using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMouseScript : MonoBehaviour
{


    [SerializeField] private float spawnLerpDelay;

    public TaskManagerScript _taskManagerScript;

    private Transform _lerpTarget1;
    private Transform _lerpTarget2;
    public Transform FocusTarget;

    private bool _firstTargetSelected = false;
    private bool _canLerp = false;

    private void Awake()
    {
        _lerpTarget1 = _taskManagerScript.SelectProcessTarget;
        _lerpTarget2 = _taskManagerScript.SelectDeleteTarget;
        FocusTarget = _lerpTarget1;

        StartCoroutine(InstantiationLerpDelay());
    }

    private void Update()
    {
        GetComponentInChildren<SpriteRenderer>().sortingOrder = Constants.Input.EffectiveLayer +1;
        ChooseTarget();
        if (_canLerp)
            LerpToTarget();
    }

    private void ChooseTarget()
    {
        if (!_firstTargetSelected)
            FocusTarget = _lerpTarget1;
        else
            FocusTarget = _lerpTarget2;

        if (transform.position == _lerpTarget1.position)
            _firstTargetSelected = true;

    }

    private void LerpToTarget()
    {
        transform.position = new Vector3
            (Mathf.Lerp(transform.position.x, FocusTarget.position.x, 0.05f),
            Mathf.Lerp(transform.position.y, FocusTarget.position.y, 0.05f),
            0);
    }

    public Transform ReturnTarget()
    {
        return FocusTarget;
    }


    IEnumerator InstantiationLerpDelay()
    {
        yield return new WaitForSeconds(spawnLerpDelay);
        _canLerp = true;
    }
}
