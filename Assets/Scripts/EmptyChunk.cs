using UnityEngine;

public class EmptyChunk : Chunk
{
    [SerializeField] private Renderer _texturePlaneRenderer;

    protected override void AssignPlaneMaterial(Material material)
    {
        _texturePlaneRenderer.material = material;
    }
}
