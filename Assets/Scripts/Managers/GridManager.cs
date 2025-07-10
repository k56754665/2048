using System;
using UnityEngine;
using static Define;

public class GridManager
{
    public event Action<int, int> OnMakeGridEvent;
    public Tile[,] Tiles => _tiles;
    private Tile[,] _tiles;

    const float TileSpacing = 10f;

    public void Init()
    {

    }

    public void MakeGrid(int width, int height)
    {
        _tiles = new Tile[width, height];

        float offsetX = (width - 1) * TileSpacing * 0.5f;
        float offsetZ = (height - 1) * TileSpacing * 0.5f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tile tile = new Tile();
                tile.CurrentTileType = Define.ETileType.None;

                float worldX = x * TileSpacing - offsetX;
                float worldZ = y * TileSpacing - offsetZ;

                tile.TilePosition = new Vector3(worldX, 0f, worldZ);

                _tiles[x, y] = tile;
            }
        }

        OnMakeGridEvent?.Invoke(width, height);
    }

    public void Clear()
    {
        _tiles = null;
    }
}
