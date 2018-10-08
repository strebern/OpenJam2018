using UnityEngine;

public class FirewallPopup : MonoBehaviour
{
    public float Speed = 0.25f;

    [SerializeField] private Transform _hidePosition;
    [SerializeField] private Transform _showPosition;

    private Transform _currentTarget;

    // CORE

    private void Awake()
    {
        _currentTarget = _hidePosition;
    }

    private void Update()
    {
        MoveToTarget();
    }

    // PUBLIC

    public void Show()
    {
        _currentTarget = _showPosition;
    }

    public void Hide()
    {
        _currentTarget = _hidePosition;
    }

    // PRIVATE

    private void MoveToTarget()
    {
        transform.position = Vector3.Lerp(transform.position, _currentTarget.position, Speed);
    }
}
