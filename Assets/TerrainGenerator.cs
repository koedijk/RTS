using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    public int mapDepth = 20;
    public int mapWidth = 256;
    public int mapHeight = 256;
    public float noiseScale = 20f;
    [Range(1, 9)]
    public int octaves;
    [Range(0, 1)]
    public float persistance;
    [Range(0, 20)]
    public float lacunarity;
    [Range(1, 99)]
    public int seed = 1;
    public bool autoUpdate;
    public Vector2 offset;

    public TerrainType[] regions;

    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    public TerrainData GenerateTerrain (TerrainData terrainData)
    {
        terrainData.heightmapResolution = mapWidth;

        terrainData.size = new Vector3(mapWidth, mapDepth, mapHeight);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[mapWidth, mapHeight];
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
        for (int x = 0; x < mapWidth; x++)
        {
          
            for (int y = 0; y < mapHeight; y++)
            {
                heights[x, y] = noiseMap[x, y];
                if (heights[x, y] > 0.5f)
                {
                    heights[x, y] = 0.5f;
                }

            }
        }
        return heights;
    }

    /*float CalculateHeight(float x, float y)
    {
        //float xCoord = (float)x / mapWidth * noiseScale;
        //float yCoord = (float)y / mapHeight * noiseScale;

        float currentHeight = noiseMap[x, z];

        return Mathf.PerlinNoise(xCoord, yCoord);
    }*/
}
