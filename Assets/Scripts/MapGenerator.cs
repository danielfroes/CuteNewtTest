
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class MapGenerator : MonoBehaviour
{
    public Tilemap groundTilemap;
    public Tilemap grassTilemap;
    public TileBase groundTile; // Array dos diferentes tipos de tiles
    public TileBase grassTile;

    public int width = 50; // Largura do mapa
    public int height = 50; // Altura do mapa
    public float scale = 5f; // Escala do ruído

    void Start()    
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * scale;
                float yCoord = (float)y / height * scale;

                float sample = Mathf.PerlinNoise(xCoord, yCoord);

                // Determinar o tipo de tile baseado na amostra do ruído
                DetermineTile(sample, x, y);
            }
        }
    }

    void DetermineTile(float value, int x, int y)
    {

        int tileMapX = x - width / 2;
        int tileMapY = y - height / 2;
        // Lógica para determinar qual tile usar com base no valor do ruído
        // Aqui você pode definir diferentes intervalos para diferentes tipos de terreno
        // Por exemplo: 0.0 a 0.3 para grama, 0.3 a 0.6 para água, etc.

        // Por enquanto, vamos simplesmente alternar entre dois tipos de terreno
        if (value < 0.6f)
        {
            grassTilemap.SetTile(new Vector3Int(tileMapX, tileMapY, 0), grassTile); 

        }
        else
        {
            groundTilemap.SetTile(new Vector3Int(tileMapX, tileMapY, 0), groundTile); 
            
        }
    }
}

