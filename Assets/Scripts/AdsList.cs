using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AdsList")]
public class AdsList : ScriptableObject
{
    public List<Window> AllAds;

    public Window GetRandomAd()
    {
        int rand = Random.Range(0, AllAds.Count);
        return AllAds[rand];
    }
}
