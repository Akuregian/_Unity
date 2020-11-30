using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise {

	// 2 Dimensional Array, used as a map.  
	// OCTAVES: controls the amount of detail. By default, each octave doubles the frequency of the last octave.
	// PERSISTANCE: determines how quickly the amplitudes diminish. Higher persistance produces rougher terrain or more amplitude which eqautes to more noise.
	// LACUNARITY: controls how fast the frequency increases, so this will get multiplied by the frequency variable.
	public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) {
		float[,] noiseMap = new float[mapWidth, mapHeight];

		// Generates Random Maps from a Seed
		System.Random prng = new System.Random(seed);
		Vector2[] octaveOffsets = new Vector2[octaves]; // Offsets for each Octave
		for (int i = 0; i < octaves; i++) {
			float offsetX = prng.Next(-100000, 100000) + offset.x; // Clamped from -100k to 100k
			float offsetY = prng.Next(-100000, 100000) + offset.y;
			octaveOffsets[i] = new Vector2(offsetX, offsetY);
		}
		
		 // Ensure scale is never less than 0, since we divide by the scale
		if (scale <= 0) {
			scale = 0.0001f;
		}
		
		// Used to normalize the values and change them back to [0 through 1], since we change it in the algorithm to [-1 through 1]
		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;

		float halfWidth = mapWidth / 2f;
		float halfHeight = mapHeight / 2f;


		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {

				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;

				for (int i = 0; i < octaves; i++) {
					float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x; // We Dont want a Integer, so We divide by scale
					float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y; // then mult the frequency + octaveOffset for extra customization
					
					// Generate the PerlinNoise && Range it from -1 to 1 by (*2 - 1). We want Negative Values for added Variations
					float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1; // ex: perlinValue = 0.00417; 0.00417(2)-1 = -.9905 {Neg Value}
					noiseHeight += perlinValue * amplitude; // Add the perlinValue * amplitude {Height} to the noiseHeight Variable
					amplitude *= persistance; // mult the amplitude by the persistance[0 to 1], which will determine How Much Influence each octave will have.
					frequency *= lacunarity; // frequency * lacunarity. which determines how quickly the frequency increases for each successive octave
				}

				// -------find Max and Min Values -------
				if (noiseHeight > maxNoiseHeight) {
					maxNoiseHeight = noiseHeight;
				}
				else if (noiseHeight < minNoiseHeight) {
					minNoiseHeight = noiseHeight;
				}

				// Set the noise map = noiseHeight, then loop again
				noiseMap[x, y] = noiseHeight;
			}
		}

		// --------- Normalize the Values back to the original 0 to 1-------------
		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
			}
		}

		return noiseMap;
	}

}


