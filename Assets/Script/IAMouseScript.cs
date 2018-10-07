using System.Collections;
using UnityEngine;

public class IAMouseScript : MonoBehaviour
{
    public float Speed = 0.05f;
    public float OnTargetDistance = 0.05f;
    public Transform FocusTarget;

    [SerializeField] private float spawnLerpDelay;
    [SerializeField] private Transform selectProcessTarget;
    [SerializeField] private Transform selectDeleteTarget;
    [SerializeField] private Transform selectTaskManagerTarget;

    private bool _firstTargetSelected = false;
    private bool _canLerp = false;

    private enum Target { Task, EndButton, TaskManager }
    private Target _currentTarget;

    // CORE

    private void Awake()
    {
        FocusTarget = selectProcessTarget;
        _currentTarget = Target.Task;
        StartCoroutine(InstantiationLerpDelay());
    }

    private void Update()
    {
        CheckIfOnTarget();
        LerpToTarget();
        Debug.LogFormat("State : {0}", _currentTarget);
    }

    // PUBLIC

    // PRIVATE

    private void CheckIfOnTarget()
    {
        if (IsOnTarget())
            ChooseNewTarget();
    }

    private void ChooseNewTarget()
    {
        switch (_currentTarget)
        {
            case Target.Task:
                FocusTarget = selectDeleteTarget;
                _currentTarget = Target.EndButton;
                break;
            case Target.EndButton:
                FocusTarget = selectTaskManagerTarget;
                _currentTarget = Target.TaskManager;
                break;
            case Target.TaskManager:
                FocusTarget = selectProcessTarget;
                _currentTarget = Target.Task;
                break;
        }
    }

    private void LerpToTarget()
    {
        if (_canLerp)
            transform.position = new Vector3
            (Mathf.Lerp(transform.position.x, FocusTarget.position.x, Speed),
            Mathf.Lerp(transform.position.y, FocusTarget.position.y, Speed),
            0);
    }

    private IEnumerator InstantiationLerpDelay()
    {
        yield return new WaitForSeconds(spawnLerpDelay);
        _canLerp = true;
    }

    private bool IsOnTarget()
    {
        return Vector3.Distance(transform.position, FocusTarget.position) < OnTargetDistance;
    }
}
