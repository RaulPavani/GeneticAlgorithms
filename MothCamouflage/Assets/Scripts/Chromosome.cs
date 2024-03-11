using UnityEngine;

public class Chromosome : MonoBehaviour
{
    //gene for colour
    private float[] color = new float[3];
    public float timeToDie = 0;

    SpriteRenderer spriteRenderer;
    Collider2D col;

    void OnMouseDown()
    {
        timeToDie = PopulationManager.currentGenerationTime;

        spriteRenderer.enabled = false;
        col.enabled = false;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        spriteRenderer.color = new Color(color[0], color[1], color[2]);
    }

    public void SetRandomColor()
    {
        color[0] = Random.Range(0f, 1f);
        color[1] = Random.Range(0f, 1f);
        color[2] = Random.Range(0f, 1f);

        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = new Color(color[0], color[1], color[2]);
    }

    public void SetColor(int index, float value)
    {
        color[index] = value;
    }

    public float GetColor(int index)
    {
        return color[index];
    }

}
