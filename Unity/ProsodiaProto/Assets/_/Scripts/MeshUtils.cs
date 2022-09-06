using System;
using UnityEngine;
public class MeshUtils
{
    public static Mesh Triangle(float size)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[3]
        {
            new Vector3(0, 0, 0),
            new Vector3(-size, 0, -size),
            new Vector3(size, 0, -size),
        };
        mesh.vertices = vertices;

        int[] tris = new int[3]
        {
            0, 2, 1,
        };
        mesh.triangles = tris;
        
        Vector3[] normals = new Vector3[3]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
        };
        mesh.normals = normals;

        return mesh;
    }
}