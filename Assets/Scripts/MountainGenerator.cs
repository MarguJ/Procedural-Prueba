using UnityEngine;

public class MountainGenerator : MonoBehaviour
{
    [SerializeField] protected MeshFilter filter;
    [SerializeField][Range(0f, 10f)] protected float scale = 0.5f;
    [SerializeField][Range(0f, 100f)] protected float height = 0.5f;

    private void Awake()
    {
        GenerateMountain();
    }

    private void GenerateMountain()
    {
        Mesh mesh = filter.mesh;
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector2 vertex = vertices[i];
            vertex.y = GetHeight(vertex.y);
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    private float GetHeight(float y)
    {
        float uai = y;
        float angleDeg = UnityEngine.Random.Range(67.5f, 112.5f);
        
        float angleRad = angleDeg * Mathf.Deg2Rad;
        float x = uai * Mathf.Cos(angleRad) / Mathf.Sin(angleRad);
        
        return x;
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
