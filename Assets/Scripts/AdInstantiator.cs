using System.Collections;
using UnityEngine;

public class AdInstantiator : MonoBehaviour
{
    [Header("Ads instanciation")]
    public int NbOfAdsInStock = 5;
    public float SecondsBetweenNewAd = 4f;
    public float DecreaseRate = 0.06f;

    [Header("Prefabs")]
    [SerializeField] private AdsList allAds;
    [SerializeField] private GameObject adPrefab;

    [Header("Events")]
    public GameObjectEvent OnAdCreation;
    public FloatEvent OnSuccessfulAd;
    public IntEvent OnAdCountChanged;

    private void Start()
    {
        StartCoroutine(AdsRegeneration());
    }

    private void Update()
    {
        if (Input.GetButtonDown(Constants.Input.Instantiate))
        {
            InstantiateAd();
        }
    }

    private void InstantiateAd()
    {
        if (NbOfAdsInStock > 0)
        {
            var ad = CreateAd();
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ad.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
            var distance = Vector3.Distance(FindObjectOfType<IAMouseScript>().transform.position, ad.transform.position);
            NbOfAdsInStock--;
            OnAdCountChanged.Invoke(NbOfAdsInStock);
            if (FindObjectOfType<GameManagerScript>().IsTaskManagerObstructed())
                OnSuccessfulAd.Invoke(distance);
        }
    }

    private GameObject CreateAd()
    {
        GameObject go = Instantiate(adPrefab);
        go.AddComponent<Ad>();
        go.layer = 8; // le layer "ads", a refaire en plus clair
        Ad ad = go.GetComponent<Ad>();
        ad.Window = allAds.GetRandomAd();
        ad.Build();
        OnAdCreation.Invoke(go);
        return go;
    }

    private IEnumerator AdsRegeneration()
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(SecondsBetweenNewAd);
            SecondsBetweenNewAd -= DecreaseRate;
            NbOfAdsInStock++;
            OnAdCountChanged.Invoke(NbOfAdsInStock);
        }
    }
}
