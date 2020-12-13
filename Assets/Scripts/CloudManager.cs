using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    [SerializeField] private Cloud[] AllClouds;
    
    private int count;

    private void Start()
    {
        count = AllClouds.Length;

        for (int i = 0; i < count; i++)
        {
            AllClouds[i].Offset = Vector2.zero;
            AllClouds[i].Material = AllClouds[i].MeshRenderer.material;
        }
    }

    private void Update()
    {
        for (int i = 0; i < count; i++)
        {
            AllClouds[i].Offset.x += AllClouds[i].Speed * Time.deltaTime;
            AllClouds[i].Material.mainTextureOffset = AllClouds[i].Offset;
        }
    }
}
