using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "new VariableSizeRuleTile", menuName = "2D/Tiles/Variable Size Rule Tile")]
public class VariableSizeRuleTile : RuleTile
{
    public Sprite Key;
    public List<Sprite> tiles;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);

        if (tileData.sprite == Key)
        {
            int mask = position.x + position.y;
            tileData.sprite = tiles[Math.Abs(mask) % 2];
        }

    }

}



