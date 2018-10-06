using UnityEngine;

public class Ad : MonoBehaviour
{
    public Window Window;

    public void Build()
    {
        gameObject.name = Window.name;
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Window.Texture;
    }
}
