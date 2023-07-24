using UnityEngine;

public sealed class ReverseNormals : MonoBehaviour
{
    private void Awake()
    {
        var meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null) return;
        
        var mesh = meshFilter.sharedMesh;
        if (mesh == null) return;
        
        var normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        mesh.normals = normals;

        var triangles = mesh.triangles;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            (triangles[i], triangles[i + 1]) = (triangles[i + 1], triangles[i]);
        }
        mesh.triangles = triangles;
    }
}