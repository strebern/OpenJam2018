﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Firewall : MonoBehaviour
{

    public Material lasermat;

    private List<GameObject> _adList = new List<GameObject>();
    [SerializeField] private Transform _lasersTransform;
    [SerializeField] private int _maxAdToRemove;
    [SerializeField] private float _secondsBetweenCleanup;
    [SerializeField] private TextMeshPro adsToRemoveText;
    [SerializeField] private LineRenderer LineRendererPreset;

    [SerializeField] private GameObject Adcutscene;

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
        ad.AddComponent<LineRenderer>();
        var lineRenderer = ad.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, ad.transform.position);
        lineRenderer.SetPosition(1, _lasersTransform.position);
        lineRenderer.material = lasermat;
        lineRenderer.startWidth = LineRendererPreset.startWidth;
        lineRenderer.endWidth = LineRendererPreset.endWidth;
        lineRenderer.startColor = LineRendererPreset.startColor;
        lineRenderer.endColor = LineRendererPreset.endColor;
        lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lineRenderer.receiveShadows = false;
    }

    private void KillAds(List<GameObject> adsToKill)
    {
        if (Adcutscene != null)
            Destroy(Adcutscene);
        foreach (GameObject ad in adsToKill)
        {
            Destroy(ad);
        }
    }

    private IEnumerator KillAdsRoutine()
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(2 * _secondsBetweenCleanup / 3);
            var adsToKill = ChooseAds();
            _firewallPopup.Show();
            yield return new WaitForSeconds(_secondsBetweenCleanup / 3);
            KillAds(adsToKill);
            _firewallPopup.Hide();
        }
    }
}
