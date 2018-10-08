using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private TaskManagerScript taskManager;
    [SerializeField] private IAMouseScript iAMouse;
    [SerializeField] private AdInstantiator adManager;
    [SerializeField] private Firewall fireWall;
    [SerializeField] int winTreshold;

    private Transform _cameraTransfrom;
    private Coroutine _winDelaycoroutine;

    private Vector3 pointChecks;
    private int _numberofCheckRun = 100;
    private int _pointAccount;

    // CORE

    private void Awake()
    {
        _cameraTransfrom = mainCamera.transform;
    }

    private void Start()
    {
       StartCoroutine(ControlActivationDelay());
    }

    private void Update()
    {
        CheckIfTaskManagerObstructed();
    }

    // PUBLIC

    public bool IsTaskManagerObstructed()
    {
        if (!iAMouse.FocusTarget) return false;
        int layer_mask = LayerMask.GetMask("Ads") + LayerMask.GetMask("OldAds");
        if (Physics.Linecast(_cameraTransfrom.position, iAMouse.FocusTarget.position, layer_mask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // PRIVATE

    private void CheckIfTaskManagerObstructed()
    {
        if (IsTaskManagerObstructed())
        {
            WinCheck();
            iAMouse.ResetTarget();
        }
        else
        {
            taskManager.CancelRelocate();
        }
    }

    private void WinCheck()
    {
        int checkIteration = 0;
        int layer_mask = LayerMask.GetMask("Ads") + LayerMask.GetMask("OldAds");

        while (checkIteration <= _numberofCheckRun)
        {
            pointChecks = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 10));
            Debug.DrawLine(_cameraTransfrom.position, pointChecks);
            if (Physics.Linecast(_cameraTransfrom.position, pointChecks, layer_mask))
            {
                _pointAccount++;
            }

            checkIteration++;
        }
        if (_pointAccount >= _numberofCheckRun - winTreshold)
            Victory();
        else
            _pointAccount = 0;
    }

    private void Victory()
    {
        Debug.Log("YOU WIN");
    }

    private void Defeat()
    {

    }

    private IEnumerator CheckVictoryDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            WinCheck();
        }
    }

    private IEnumerator ControlActivationDelay()
    {
        yield return new WaitForSeconds(20);
        adManager.enabled = true;
        fireWall.enabled = true;
    }
}
