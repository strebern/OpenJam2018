﻿using UnityEngine;

public class AdInstantiator : MonoBehaviour
{
    public AdsList adsList;
    public GameObject adPrefab;

    private void Update()
    {
        if (Input.GetButtonDown(Constants.Input.Instantiate))
        {
            InstantiateAd();
        }
    }

    private void InstantiateAd()
    {
        var ad = CreateAd();
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ad.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
    }

    private GameObject CreateAd()
    {
        GameObject go = Instantiate(adPrefab);
        go.AddComponent<Ad>();
        Ad ad = go.GetComponent<Ad>();
        ad.Window = adsList.GetRandomAd();
        ad.Build();
        return go;
    }
}