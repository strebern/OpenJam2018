using UnityEngine;

[CreateAssetMenu(menuName = "Windows/Simple Window")]
public class Window : ScriptableObject
{
    public string Name;
    public Vector2 Resolution;
    public Sprite Texture;
    public AudioClip PopSound;
}
