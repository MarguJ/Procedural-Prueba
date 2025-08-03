using UnityEngine;

public class MountainGenerator : MonoBehaviour
{
    [SerializeField] protected MeshFilter _filter;
    [SerializeField][Range(0f, 10f)] protected float _scale = 0.5f;
    [SerializeField][Range(0f, 100f)] protected float _height = 0.5f;
    [SerializeField] protected Vector2 _offset = Vector2.one;

    private void Awake()
    {
        GenerateMountain();
    }

    private void GenerateMountain()
    {
        Mesh mesh = _filter.mesh;
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = vertices[i];
            vertex.y = GetHeight(vertex.x, vertex.z);
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    private float GetHeight(float x, float z)
    {
        x += _offset.x;
        z += _offset.y;
        return Mathf.PerlinNoise(x / _filter.mesh.bounds.size.x * _scale, z / _filter.mesh.bounds.size.z * _scale) * _height;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        GenerateMountain();
    }
}
