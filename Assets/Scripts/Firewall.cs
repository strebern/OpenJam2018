using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Firewall : MonoBehaviour
{
    private List<GameObject> _adList = new List<GameObject>();
    [SerializeField] private int _maxAdToRemove;
    [SerializeField] private float _secondsBetweenCleanup;
    [SerializeField] private TextMeshPro adsToRemoveText;

    private FirewallPopup _firewallPopup;

    // CORE

    private void Awake()
    {
        _firewallPopup = GetComponentInChildren<FirewallPopup>();
    }

    private void Start()
    {
        StartCoroutine(KillAdsRoutine());
    }

    // PUBLIC

    public void AddAd(GameObject ad)
    {
        _adList.Add(ad);
    }

    // PRIVATE

    private List<GameObject> ChooseAds()
    {
        Debug.Log("Choosing ads");
        List<GameObject> adsToKill = new List<GameObject>();
        int nbOfAdsToKill = Random.Range(1, _maxAdToRemove);
        adsToRemoveText.text = "" + nbOfAdsToKill;
        for (int i = 0; i < nbOfAdsToKill; i++)
        {
            if (_adList.Count > 0)
            {
                var ad = _adList[0];
                _adList.Remove(ad);
                adsToKill.Add(ad);
                TraceLineToAd(ad);
            }
        }
        return adsToKill;
    }

    private void TraceLineToAd(GameObject ad)
    {
        Debug.Log("Tracing lines");
        ad.AddComponent<LineRenderer>();
        var lineRenderer = ad.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, ad.transform.position);
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;
        lineRenderer.renderingLayerMask = 3;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
    }

    private void KillAds(List<GameObject> adsToKill)
    {
        foreach(GameObject ad in adsToKill)
        {
            Destroy(ad);
        }
    }

    private IEnumerator KillAdsRoutine()
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(2*_secondsBetweenCleanup/3);
            var adsToKill = ChooseAds();
            _firewallPopup.Show();
            yield return new WaitForSeconds(_secondsBetweenCleanup/3);
            KillAds(adsToKill);
            _firewallPopup.Hide();
        }
    }
}
