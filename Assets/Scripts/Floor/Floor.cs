using UnityEngine;

public class Floor : MonoBehaviour
{
    void Start()
    {
        Managers.Grid.OnMakeGridEvent += SetFloorSize;
    }

    void SetFloorSize(int width, int height)
    {
        transform.localScale = new Vector3(width, 1, height);
    }

    void OnDestroy()
    {
        Managers.Grid.OnMakeGridEvent -= SetFloorSize;
    }
}
