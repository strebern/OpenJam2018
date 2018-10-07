using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    [SerializeField] private GameObject mainCamera;
    [SerializeField] private TaskManagerScript taskManager;
    [SerializeField] private IAMouseScript iAMouse;
    [SerializeField] int winTreshold;

    private Transform _cameraTransfrom;
    private Coroutine _winDelaycoroutine;

    private Vector3 pointChecks;
    private int _numberofCheckRun = 100;
    private int _pointAccount;

    private void Awake()
    {
        _cameraTransfrom = mainCamera.transform;
    }

    private void Start()
    {
       // _winDelaycoroutine = StartCoroutine(CheckVictoryDelay());
    }

    private void Update()
    {
        IsTaskManagerObstructed(iAMouse.FocusTarget);
    }

    public void IsTaskManagerObstructed(Transform targetPosition)
    {
        int layer_mask = LayerMask.GetMask("Ads");
        if (Physics.Linecast(_cameraTransfrom.position, targetPosition.position, layer_mask))
        {
            WinCheck();
            taskManager.InitializeRelocate();
            iAMouse.ResetTarget();
        }
        else
        {
            taskManager.CancelRelocate();
        }
        int layer_mask2 = LayerMask.GetMask("OldAds");
        if (Physics.Linecast(_cameraTransfrom.position, targetPosition.position, layer_mask))
        {
            taskManager.PutTaskManagerInFront();
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
     //   StopCoroutine(_winDelaycoroutine);
    }

    private IEnumerator CheckVictoryDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            WinCheck();
        }
    }
}
