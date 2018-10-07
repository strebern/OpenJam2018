using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    [SerializeField] GameObject mainCamera;
    [SerializeField] TaskManagerScript taskManager;
    [SerializeField] IAMouseScript iAMouse;
    // [SerializeField] GameObject adManager;

    [SerializeField] int winTreshold;


    private Transform _cameraTransfrom;

    private Vector3 pointChecks;
    private int _numberofCheckRun = 100;
    private int _pointAccount;
    private bool _won = false;

    private void Awake()
    {
        _cameraTransfrom = mainCamera.transform;
    }

    private void Start()
    {
        StartCoroutine(CheckVictoryDelay());
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
            taskManager.InitializeRelocate();
        }
        else
        {
            taskManager.Cancelrelocate();
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
                Debug.Log(_pointAccount);
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
        _won = true;
    }

    IEnumerator CheckVictoryDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (!_won)
                WinCheck();
        }
    }
}
