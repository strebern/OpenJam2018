using System.Collections;
using UnityEngine;
using UnityEngine.Events;



public class IAMouseScript : MonoBehaviour
{
    public float Speed = 0.05f;
    public float OnTargetDistance = 0.05f;
    public Transform FocusTarget;

    public UnityEvent LoseGame;

    [SerializeField] private float spawnLerpDelay;
    [SerializeField] private GameObject loseScreen;
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
    }

    // PUBLIC

    public void ResetTarget()
    {
        _currentTarget = Target.TaskManager;
        FocusTarget = selectTaskManagerTarget;
    }

    public bool MovingToEndOrTask()
    {
        return _currentTarget == Target.EndButton || _currentTarget == Target.Task;
    }

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
                LoseGame.Invoke();
                loseScreen.SetActive(true);
                _currentTarget = Target.TaskManager;
                FindObjectOfType<MusicPlayer>().PlayLoseMusic();
                Destroy(gameObject);
                break;
            case Target.TaskManager:
                FindObjectOfType<TaskManagerScript>().RelocateTaskManager();
                FocusTarget = selectProcessTarget;
                _currentTarget = Target.Task;
                break;
        }
    }

    private void LerpToTarget()
    {
        if (_canLerp && FocusTarget)
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
        if (FocusTarget)
            return Vector3.Distance(transform.position, FocusTarget.position) < OnTargetDistance;
        else
            return false;
    }
}
