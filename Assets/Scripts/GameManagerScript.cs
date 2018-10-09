using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private TaskManagerScript taskManager;
    [SerializeField] private IAMouseScript iAMouse;
    [SerializeField] private AdInstantiator adManager;
    [SerializeField] private Firewall fireWall;
    [SerializeField] private FirewallPopup fireWallPopup;


    [SerializeField] private int _gameTimer;
    [SerializeField] int winTreshold;

    public UnityEvent LaunchGame;
    public UnityEvent StartGame;
    public UnityEvent WinGame;

    private Transform _cameraTransfrom;
    private Coroutine _winDelaycoroutine;

    private Vector3 pointChecks;
    private int _numberofCheckRun = 100;
    private int _pointAccount;
    public bool dontDisableControl;

    // CORE

    private void Awake()
    {
        _cameraTransfrom = mainCamera.transform;
    }

    private void Start()
    {
        LaunchGame.Invoke();
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
        SceneManager.LoadScene("WinScreen");
        WinGame.Invoke();
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
        StartGame.Invoke();
    }

    private IEnumerator Gametimer()
    {
        yield return new WaitForSeconds(_gameTimer);
    }

}
