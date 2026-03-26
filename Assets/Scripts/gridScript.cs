using UnityEngine;

public class gridScript : MonoBehaviour
{
    [SerializeField] float scrollSpeedX = 0f;
    [SerializeField] float scrollSpeedY = 0.6f;
    private Renderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float offsetX = Time.time * scrollSpeedX;
        float offsetY = Time.time * scrollSpeedY;
        meshRenderer.material.SetTextureOffset("_BaseMap", new Vector2(offsetX, offsetY));
    }
}
