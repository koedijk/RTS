using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerate : MonoBehaviour
{
    [SerializeField]
    public MapGenerator map;
    [SerializeField]
    private List<GameObject> ArrayObj = new List<GameObject>();
    public GameObject obj;

    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    [Range(1, 9)]
    public int octaves;
    [Range(0, 1)]
    public float persistance;
    [Range(0, 20)]
    public float lacunarity;
    [Range(1, 99)]
    public int seed = 1;
    public Vector2 offset;

    public TerrainType[] regions;
    // Use this for initialization
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth,mapHeight,seed,noiseScale,octaves,persistance,lacunarity,offset);
        Color[] colourMap = new Color[mapWidth * mapHeight];
        for (int z = 0; z < mapHeight; z++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, z];
                obj = Instantiate(obj, new Vector3(obj.transform.localScale.x*x, 0, obj.transform.localScale.z*z), Quaternion.identity);
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        obj.GetComponent<Renderer>().material.color = regions[i].colour;
                        break;
                    }
                }                      
                ArrayObj.Add(obj);

            }
        }

    }

    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color colour;
    }
}

