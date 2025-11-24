using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    public Texture2D map;

    public ColourToPrefab[] colourMaps;
    void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        for(int x = 0; x < map.width; x++)
        {
            for(int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    public void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
        {
            return;
        }

        foreach(ColourToPrefab colourMapping in colourMaps)
        {
            if(colourMapping.color.Equals(pixelColor))
            {
                Vector2 position = new Vector2(x, y);
                Instantiate(colourMapping.prefab, position, Quaternion.identity);
            }
        }
    }
}
