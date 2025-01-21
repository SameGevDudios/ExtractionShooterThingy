
// Workaround for forcing GPU Instance enable
// Credit goes to яковлев »ль€: https://www.youtube.com/watch?v=JM5II7wpF4M

using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class GPUInstancingEnabler : MonoBehaviour
{
    private void Awake()
    {
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.SetPropertyBlock(block);
    }
}
