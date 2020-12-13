using UnityEngine;

[System.Serializable]
public class Cloud
{
    public MeshRenderer MeshRenderer;
    public float Speed = 0f;
    [HideInInspector] public Vector2 Offset;
    [HideInInspector] public Material Material;
}
