using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int depth = 50;  // Vertical scale of the terrain
    public int width = 256; // Horizontal size of the terrain
    public int height = 256; // Depth size of the terrain
    public float scale = 20f; // Base scale for Perlin noise
    public int octaves = 4; // Number of noise layers
    public float baseFrequency = 1f; // Base frequency for the noise
    public float baseAmplitude = 1f; // Base amplitude for the noise
    public float lacunarity = 2f; // Multiplier for frequency between octaves
    public float persistence = 0.5f; // Multiplier for amplitude between octaves
    public int erosionIterations = 10000; // Number of water droplets simulated
    public float erosionStrength = 0.02f; // Strength of erosion effect
    public float depositionStrength = 0.01f; // Strength of sediment deposition
    public float minSlope = 0.01f; // Minimum slope to trigger erosion

    private float[,] heightMap;

    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        heightMap = GenerateHeights();
        heightMap = ApplyHydraulicErosion(heightMap);
        terrainData.SetHeights(0, 0, heightMap);
        return terrainData;
    }

    float[,] GenerateHeights()
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

    float CalculatefBMHeight(int x, int y)
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

    float[,] ApplyHydraulicErosion(float[,] heights)
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
                    // Erode
                    float erosionAmount = slope * erosionStrength;
                    heights[x, y] -= erosionAmount;

                    // Deposit sediment
                    heights[lowestNeighbor.x, lowestNeighbor.y] += erosionAmount * depositionStrength;
                }
            }
        }
        return heights;
    }

    Vector2Int GetLowestNeighbor(float[,] heights, int x, int y)
    {
        Vector2Int lowest = new Vector2Int(x, y);
        float lowestHeight = heights[x, y];

        // Check all neighboring cells
        for (int nx = -1; nx <= 1; nx++)
        {
            for (int ny = -1; ny <= 1; ny++)
            {
                if (nx == 0 && ny == 0) continue; // Skip the current cell
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