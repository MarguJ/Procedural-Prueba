using UnityEditor;
using UnityEngine;

public class TerrainGeneratorEditor : EditorWindow
{
    float finalFNumber;
    int finalINumber;
    // Terrain parameters
    private Terrain terrain;
    private int depth; //Puede tomar valores entre 10 y 100 (Solo enteros)
    private int width; //Cambia el largo y el ancho del terreno, tiene que ser mismo valor que Height
    private int height; //Cambia el largo y el ancho del terreno, tiene que ser mismo valor que Width
    private float scale; //Puede tomar valores entre 10 y 100 (No solo enteros)

    // fBM parameters
    private int octaves; //Puede tomar valores entre 1 y 10 (Solo enteros)
    private float baseFrequency; //Puede tomar valores entre 1 y 5 (No solo enteros)
    private float baseAmplitude; //Puede tomar valores entre 0,1 y 2 (No solo enteros)
    private float lacunarity; //Puede tomar valores entre 0 y 7 (No solo enteros)
    private float persistence = 0.5f;

    // Hydraulic erosion parameters
    private int erosionIterations = 10000; //No entiendo bien que cambian por eso los dejo en el mismo valor
    private float erosionStrength = 0.02f; //No entiendo bien que cambian por eso los dejo en el mismo valor
    private float depositionStrength = 0.01f; //No entiendo bien que cambian por eso los dejo en el mismo valor
    private float minSlope = 0.01f; //No entiendo bien que cambian por eso los dejo en el mismo valor

    [MenuItem("Tools/Terrain Generator")]
    public static void ShowWindow()
    {
        GetWindow<TerrainGeneratorEditor>("Terrain Generator");
    }
    public float GetTerrainVertexHeight(int x, int y)
    {
        if (terrain == null || terrain.terrainData == null)
        {
            Debug.LogError("Terrain or TerrainData is not assigned.");
            return 0f;
        }

        int hmWidth = terrain.terrainData.heightmapResolution;
        int hmHeight = terrain.terrainData.heightmapResolution;

        if (x < 0 || x >= hmWidth || y < 0 || y >= hmHeight)
        {
            Debug.LogError("Vertex coordinates out of bounds.");
            return 0f;
        }

        float[,] heights = terrain.terrainData.GetHeights(x, y, 1, 1);
        return heights[0, 0];
    }
    private float GetRandomFloat(float min, float max)
    {
        if (min > max)
        {
            Debug.LogError("Min is higher than Max. Change it.");
        }
        finalFNumber = Random.Range(min, max);
        return finalFNumber;
    }
    private int GetRandomInt(int min, int max)
    {
        if (min > max)
        {
            Debug.LogError("Min is higher than Max. Change it.");
        }
        finalINumber = Random.Range(min, max);
        return finalINumber;
    }
    private void OnGUI()
    {
        GUILayout.Label("Terrain Generator Settings", EditorStyles.boldLabel);

        // Terrain object
        terrain = (Terrain)EditorGUILayout.ObjectField("Terrain", terrain, typeof(Terrain), true);

        width = EditorGUILayout.IntField("Width", width);
        height = EditorGUILayout.IntField("Height", height);
        depth = GetRandomInt(10, 100);
        scale = GetRandomFloat(10, 100);

        octaves = GetRandomInt(1, 10);
        baseFrequency = GetRandomFloat(1, 5);
        baseAmplitude = GetRandomFloat(0.1f, 2);
        lacunarity = GetRandomFloat(0, 7);
        persistence = 0.1f;

        GUILayout.Space(10);
        GUILayout.Label("Hydraulic Erosion", EditorStyles.boldLabel);
        erosionIterations = EditorGUILayout.IntField("Erosion Iterations", erosionIterations);
        erosionStrength = EditorGUILayout.FloatField("Erosion Strength", erosionStrength);
        depositionStrength = EditorGUILayout.FloatField("Deposition Strength", depositionStrength);
        minSlope = EditorGUILayout.FloatField("Min Slope", minSlope);

        GUILayout.Space(20);

        if (GUILayout.Button("Generate Terrain"))
        {
            if (terrain == null)
            {
                Debug.LogError("Please assign a Terrain object.");
            }
            else
            {
                GenerateTerrain();
            }
        }
    }

    private void GenerateTerrain()
    {
        TerrainData terrainData = terrain.terrainData;

        // Initialize terrain dimensions
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);

        // Generate heightmap using fBM
        float[,] heights = GenerateHeights();

        // Apply hydraulic erosion
        heights = ApplyHydraulicErosion(heights);

        // Set heights to the terrain
        terrainData.SetHeights(0, 0, heights);
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculatefBMHeight(x, y);
            }
        }
        return heights;
    }

    private float CalculatefBMHeight(int x, int y)
    {
        float total = 0;
        float frequency = baseFrequency / width;
        float amplitude = baseAmplitude;

        for (int i = 0; i < octaves; i++)
        {
            float xCoord = x * frequency * scale;
            float yCoord = y * frequency * scale;
            total += Mathf.PerlinNoise(xCoord, yCoord) * amplitude;

            frequency *= lacunarity;
            amplitude *= persistence;
        }

        return Mathf.Clamp01(total);
    }

    private float[,] ApplyHydraulicErosion(float[,] heights)
    {
        for (int i = 0; i < erosionIterations; i++)
        {
            int x = Random.Range(1, width - 1);
            int y = Random.Range(1, height - 1);

            float waterHeight = heights[x, y];
            Vector2Int lowestNeighbor = GetLowestNeighbor(heights, x, y);

            if (lowestNeighbor != new Vector2Int(x, y))
            {
                float slope = waterHeight - heights[lowestNeighbor.x, lowestNeighbor.y];
                if (slope > minSlope)
                {
                    float erosionAmount = slope * erosionStrength;
                    heights[x, y] -= erosionAmount;
                    heights[lowestNeighbor.x, lowestNeighbor.y] += erosionAmount * depositionStrength;
                }
            }
        }
        return heights;
    }

    private Vector2Int GetLowestNeighbor(float[,] heights, int x, int y)
    {
        Vector2Int lowest = new Vector2Int(x, y);
        float lowestHeight = heights[x, y];

        for (int nx = -1; nx <= 1; nx++)
        {
            for (int ny = -1; ny <= 1; ny++)
            {
                if (nx == 0 && ny == 0) continue;

                int neighborX = x + nx;
                int neighborY = y + ny;

                if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
                {
                    float neighborHeight = heights[neighborX, neighborY];
                    if (neighborHeight < lowestHeight)
                    {
                        lowest = new Vector2Int(neighborX, neighborY);
                        lowestHeight = neighborHeight;
                    }
                }
            }
        }
        return lowest;
    }
}