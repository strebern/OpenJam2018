using System.Collections;
using UnityEngine;

public class TaskManagerScript : MonoBehaviour
{
    [SerializeField] private float timeBeforeRelocate;

    private bool _targetIsObstructed = false;
    private bool _isRelocateCouroutineStarted = false;
    private Coroutine _obstructedTimerCoroutine;
    private Coroutine _putInFrontDelay;

    // CORE

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        {
            //RelocateTaskManager();
        }
    }

    // PUBLIC

    public void PutTaskManagerInFront()
    {
        if (_putInFrontDelay != null)
            StopCoroutine(_putInFrontDelay);
        _putInFrontDelay = StartCoroutine(PutInFrontDelay());
    }

    public void CancelRelocate()
    {
        if (_isRelocateCouroutineStarted)
        {
            StopCoroutine(_obstructedTimerCoroutine);
            _isRelocateCouroutineStarted = false;
        }
        _targetIsObstructed = false;
    }

    public void RelocateTaskManager()
    {
        Constants.Input.EffectiveLayer++;
        Vector3 randomPoint = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(100, Screen.width - 100), Random.Range(100, Screen.height - 100), 10));
        transform.position = randomPoint;
        _isRelocateCouroutineStarted = false;
        GetComponentInChildren<SpriteRenderer>().sortingOrder = Constants.Input.EffectiveLayer;
    }

    // PRIVATE

    private IEnumerator PutInFrontDelay()
    {
        yield return new WaitForSeconds(timeBeforeRelocate);
        Constants.Input.EffectiveLayer++;
        GetComponentInChildren<SpriteRenderer>().sortingOrder = Constants.Input.EffectiveLayer;
    }
}
