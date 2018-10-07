using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManagerScript : MonoBehaviour {

    public Transform SelectProcessTarget;
    public Transform SelectDeleteTarget;

    [SerializeField] private float timeBeforeRelocate;
    [SerializeField] private Camera playerCamera;

    private bool _targetIsObstructed = false;
    private bool _isRelocateCouroutineStarted = false;
    private Coroutine _obstructedTimerCoroutine;

    private void Awake()
    {
        SelectProcessTarget = SelectProcessTarget.transform;
    }

    public void IsTargetObstructed(Transform targetPosition)
    {
        var cameraTransform = playerCamera.transform.position;
        int layer_mask = LayerMask.GetMask("Ads");

        if (Physics.Linecast(cameraTransform, targetPosition.position, layer_mask))
        {
            _targetIsObstructed = true;
            if (!_isRelocateCouroutineStarted)
            {
                _obstructedTimerCoroutine = StartCoroutine(TimeObstructedBeforeRelocate());
                _isRelocateCouroutineStarted = true;
            }
        }
        else
        {
            if (_isRelocateCouroutineStarted)
            {
                StopCoroutine(_obstructedTimerCoroutine);
                _isRelocateCouroutineStarted = false;
            }
            _targetIsObstructed = false;
        }
    }

    public void RelocateTaskManager()
    {
        transform.position = new Vector3(Random.Range(-9, 9), Random.Range(-5, 5), 0);
        _isRelocateCouroutineStarted = false;
    }

    IEnumerator TimeObstructedBeforeRelocate()
    {
        yield return new WaitForSeconds(timeBeforeRelocate);
        RelocateTaskManager();
    }
}
