using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class OrderInLayer : MonoBehaviour
{
    [SerializeField] private int orderInLayer;
    [SerializeField] private MeshRenderer text;

    private void Awake()
    {
        text.sortingLayerName = "DesktopBar";
        text.sortingOrder = orderInLayer;
    }
}
