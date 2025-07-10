using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BoardManager
{
    Block[,] _blocks;

    public void Init()
    {
        var tiles = Managers.Grid.Tiles;
        int width = tiles.GetLength(0);
        int height = tiles.GetLength(1);
        _blocks = new Block[width, height];

        Managers.Input.OnInputEvent += OnInput;

        SpawnRandomBlock();
        SpawnRandomBlock();
    }

    public void Clear()
    {
        Managers.Input.OnInputEvent -= OnInput;
        _blocks = null;
    }

    void OnInput(EInputType input)
    {
        bool moved = false;
        switch (input)
        {
            case EInputType.Up:
                moved = MoveUp();
                break;
            case EInputType.Down:
                moved = MoveDown();
                break;
            case EInputType.Left:
                moved = MoveLeft();
                break;
            case EInputType.Right:
                moved = MoveRight();
                break;
        }

        if (moved)
            SpawnRandomBlock();
    }

    bool SpawnRandomBlock()
    {
        if (_blocks == null)
            return false;

        int width = _blocks.GetLength(0);
        int height = _blocks.GetLength(1);
        List<Vector2Int> empty = new List<Vector2Int>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (_blocks[x, y] == null)
                    empty.Add(new Vector2Int(x, y));
            }
        }
        if (empty.Count == 0)
            return false;

        Vector2Int pos = empty[Random.Range(0, empty.Count)];
        Vector3 world = Managers.Grid.Tiles[pos.x, pos.y].TilePosition;
        Block block = Managers.Spawn.SpawnRandomBlock(world);
        if (block == null)
            return false;

        block.Init(2, 1, false);
        _blocks[pos.x, pos.y] = block;
        return true;
    }

    bool MoveUp()
    {
        bool moved = false;
        int width = _blocks.GetLength(0);
        int height = _blocks.GetLength(1);
        bool[,] merged = new bool[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = height - 2; y >= 0; y--)
            {
                Block b = _blocks[x, y];
                if (b == null) continue;
                int ny = y;
                while (ny + 1 < height && _blocks[x, ny + 1] == null)
                    ny++;
                if (ny + 1 < height && _blocks[x, ny + 1] != null && !merged[x, ny + 1])
                {
                    _blocks[x, ny + 1].Init(CalculateAttack(_blocks[x, ny + 1].Attack, b.Attack), CalculateHp(_blocks[x, ny + 1].Hp, b.Hp), false);
                    Object.Destroy(b.gameObject);
                    _blocks[x, y] = null;
                    merged[x, ny + 1] = true;
                    moved = true;
                }
                else
                {
                    if (ny != y)
                    {
                        _blocks[x, ny] = b;
                        _blocks[x, y] = null;
                        b.transform.position = Managers.Grid.Tiles[x, ny].TilePosition;
                        moved = true;
                    }
                }
            }
        }
        return moved;
    }

    bool MoveDown()
    {
        bool moved = false;
        int width = _blocks.GetLength(0);
        int height = _blocks.GetLength(1);
        bool[,] merged = new bool[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 1; y < height; y++)
            {
                Block b = _blocks[x, y];
                if (b == null) continue;
                int ny = y;
                while (ny - 1 >= 0 && _blocks[x, ny - 1] == null)
                    ny--;
                if (ny - 1 >= 0 && _blocks[x, ny - 1] != null && !merged[x, ny - 1])
                {
                    _blocks[x, ny - 1].Init(CalculateAttack(_blocks[x, ny - 1].Attack, b.Attack), CalculateHp(_blocks[x, ny - 1].Hp, b.Hp), false);
                    Object.Destroy(b.gameObject);
                    _blocks[x, y] = null;
                    merged[x, ny - 1] = true;
                    moved = true;
                }
                else
                {
                    if (ny != y)
                    {
                        _blocks[x, ny] = b;
                        _blocks[x, y] = null;
                        b.transform.position = Managers.Grid.Tiles[x, ny].TilePosition;
                        moved = true;
                    }
                }
            }
        }
        return moved;
    }

    bool MoveLeft()
    {
        bool moved = false;
        int width = _blocks.GetLength(0);
        int height = _blocks.GetLength(1);
        bool[,] merged = new bool[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 1; x < width; x++)
            {
                Block b = _blocks[x, y];
                if (b == null) continue;
                int nx = x;
                while (nx - 1 >= 0 && _blocks[nx - 1, y] == null)
                    nx--;
                if (nx - 1 >= 0 && _blocks[nx - 1, y] != null && !merged[nx - 1, y])
                {
                    _blocks[nx - 1, y].Init(CalculateAttack(_blocks[nx - 1, y].Attack, b.Attack), CalculateHp(_blocks[nx - 1, y].Hp, b.Hp), false);
                    Object.Destroy(b.gameObject);
                    _blocks[x, y] = null;
                    merged[nx - 1, y] = true;
                    moved = true;
                }
                else
                {
                    if (nx != x)
                    {
                        _blocks[nx, y] = b;
                        _blocks[x, y] = null;
                        b.transform.position = Managers.Grid.Tiles[nx, y].TilePosition;
                        moved = true;
                    }
                }
            }
        }
        return moved;
    }

    bool MoveRight()
    {
        bool moved = false;
        int width = _blocks.GetLength(0);
        int height = _blocks.GetLength(1);
        bool[,] merged = new bool[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = width - 2; x >= 0; x--)
            {
                Block b = _blocks[x, y];
                if (b == null) continue;
                int nx = x;
                while (nx + 1 < width && _blocks[nx + 1, y] == null)
                    nx++;
                if (nx + 1 < width && _blocks[nx + 1, y] != null && !merged[nx + 1, y])
                {
                    _blocks[nx + 1, y].Init(CalculateAttack(_blocks[nx + 1, y].Attack, b.Attack), CalculateHp(_blocks[nx + 1, y].Hp, b.Hp), false);
                    Object.Destroy(b.gameObject);
                    _blocks[x, y] = null;
                    merged[nx + 1, y] = true;
                    moved = true;
                }
                else
                {
                    if (nx != x)
                    {
                        _blocks[nx, y] = b;
                        _blocks[x, y] = null;
                        b.transform.position = Managers.Grid.Tiles[nx, y].TilePosition;
                        moved = true;
                    }
                }
            }
        }
        return moved;
    }

    int CalculateAttack(int a, int b)
    {
        return a + b;
    }

    int CalculateHp(int a, int b)
    {
        return Mathf.Max(a, b);
    }
}