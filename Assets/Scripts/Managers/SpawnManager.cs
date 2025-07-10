using UnityEngine;

public class SpawnManager
{
    GameObject[] _blockPrefabs;

    public void Init()
    {
        _blockPrefabs = Resources.LoadAll<GameObject>("Block");
    }

    GameObject GetRandomBlock()
    {
        if (_blockPrefabs == null || _blockPrefabs.Length == 0)
            Init();

        if (_blockPrefabs.Length == 0)
            return null;

        int index = Random.Range(0, _blockPrefabs.Length);
        return _blockPrefabs[index];
    }

    public Block SpawnRandomBlock(Vector3 position)
    {
        GameObject prefab = GetRandomBlock();
        if (prefab == null)
            return null;

        GameObject go = Object.Instantiate(prefab, position, Quaternion.identity);
        return go.GetComponent<Block>();
    }

    public Block SpawnRandomBlockOnGrid()
    {
        if (Managers.Grid.Tiles == null)
            return null;

        int width = Managers.Grid.Tiles.GetLength(0);
        int height = Managers.Grid.Tiles.GetLength(1);

        int x = Random.Range(0, width);
        int y = Random.Range(0, height);

        Vector3 pos = Managers.Grid.Tiles[x, y].TilePosition;
        return SpawnRandomBlock(pos);
    }
}
