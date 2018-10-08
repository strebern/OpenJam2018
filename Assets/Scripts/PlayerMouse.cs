using UnityEngine;
using TMPro;

public class PlayerMouse : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private TextMeshPro _adCountText;

    // CORE

    private void Update()
    {
        StickToMouse();
    }

    // PUBLIC

    public void UpdateAdCount(int adCount)
    {
        _adCountText.text = "" + adCount;
    }

    // PRIVATE

    private void StickToMouse()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        transform.position = mousePosition + _offset;
    }
}
