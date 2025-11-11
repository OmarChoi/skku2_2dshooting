using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public Material BackgroundMaterial = null;
    public float ScrollSpeed = 0.1f;

    private void Update()
    {
        Vector2 direction = Vector2.up;
        Vector2 offset = BackgroundMaterial.mainTextureOffset;

        offset += direction * ScrollSpeed * Time.deltaTime;
        offset.y = Mathf.Repeat(offset.y, 1.0f);

        BackgroundMaterial.mainTextureOffset = offset;
    }
}
