using UnityEngine;

public class Ad : MonoBehaviour
{
    public Window Window;

    private void Update()
    {
        if(GetComponent<SpriteRenderer>().sortingOrder < Constants.Input.EffectiveLayer)
        {
          gameObject.layer = 9;
        }
    }

    public void Build()
    {
        Constants.Input.EffectiveLayer++;
        gameObject.name = Window.name;
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Window.Texture;
        spriteRenderer.sortingOrder = Constants.Input.EffectiveLayer;
    }
}
