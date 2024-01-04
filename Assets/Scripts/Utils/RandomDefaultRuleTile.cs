using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

namespace CuteNewtTest.Utils
{
    [CreateAssetMenu(fileName = "new RandomDefaultRuleTile", menuName = "CustomTiles/Random Default Rule Tile")]
    public class RandomDefaultRuleTile : RuleTile
    {
        [SerializeField, Range(0, 100)] int _defaultTileProbability = 95;
        [SerializeField] List<Sprite> _defaultTileVariants;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetTileData(position, tilemap, ref tileData);

            if (_defaultTileVariants.IsNullOrEmpty() || tileData.sprite != m_DefaultSprite)
                return;

            tileData.sprite = ChooseMainTileSprite();
        }

        public Sprite ChooseMainTileSprite()
        {
            return Random.value < _defaultTileProbability / 100f ? m_DefaultSprite : _defaultTileVariants.GetRandom();
        }
    }
}
