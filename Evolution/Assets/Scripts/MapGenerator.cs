using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MapGenerator Class used the noise class, that returns a 2D PerlinNoise Map and then
// used MapDisplay class to draw a texture to that map
public class MapGenerator : MonoBehaviour {

    public enum DrawMode { NoiseMap, ColorMap }
    public DrawMode drawMode;
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public int seed;
    public Vector2 offSet;

    public bool autoUpdate;

    public TerrainType[] regions;

    public void GenerateMap() {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offSet); // Generate noiseMap

        Color[] colorMap = new Color[mapWidth*mapHeight];
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++) {
                    if (currentHeight <= regions[i].height) {
                        colorMap[y * mapWidth + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>(); // Find the MapDisplay class, to display Texure
        if (drawMode == DrawMode.NoiseMap) {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));  // Draws colors/textures
        } else if(drawMode == DrawMode.ColorMap) {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));  // Draws colors/textures
        }

    }

    private void OnValidate() { // Function from Unity that is ran everytime the scipt is loaded or a value in the inspector changed
        if(mapWidth < 1) {
            mapWidth = 1;
        }
        
        if(mapHeight < 1) {
            mapHeight = 1;
        }

        if(lacunarity < 1) {
            lacunarity = 1;
        }

        if(octaves < 0) {
            octaves = 0;
        }
    }
}

[System.Serializable]
public struct TerrainType {
    public string name;
    public float height;
    public Color color;
}
