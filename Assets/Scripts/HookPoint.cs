using UnityEngine;

public class HookPoint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isInRange;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
    }

    public void SetInRange(bool inRange)
    {
        isInRange = inRange;
        UpdateColor();
    }

    public void SetSelected(bool selected)
    {
        if (selected)
        {
            spriteRenderer.color = Color.red;
        }
        else if (isInRange)
        {
            spriteRenderer.color = Color.yellow; // Or any other color you prefer for "in range but not selected"
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    private void UpdateColor()
    {
        spriteRenderer.color = isInRange ? Color.yellow : Color.white;
    }
}
