using UnityEngine;

public class MountainGenerator : MonoBehaviour
{
    [SerializeField] protected MeshFilter _filter;
    [SerializeField][Range(0f, 10f)] protected float _scale = 0.5f;
    [SerializeField][Range(0f, 100f)] protected float _height = 0.5f;

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
            Vector2 vertex = vertices[i];
            vertex.y = GetHeight(vertex.x, vertex.y);
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    private float GetHeight(float x, float y)
    {
        float angleDeg = UnityEngine.Random.Range(67.5f, 112.5f);
        
        float angleRad = angleDeg * Mathf.Deg2Rad;
        x = y * Mathf.Cos(angleRad) / Mathf.Sin(angleRad);
        return (x,y);
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
