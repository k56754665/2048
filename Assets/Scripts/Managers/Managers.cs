using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    public static Managers Instance => _instance;
    static Managers _instance;

    public static GridManager Grid => _gridManager;
    static GridManager _gridManager = new GridManager();

    public static InputManager Input => _inputManager;
    static InputManager _inputManager = new InputManager();

    public static SpawnManager Spawn => _spawnManager;
    static SpawnManager _spawnManager = new SpawnManager();

    public static BoardManager Board => _boardManager;
    static BoardManager _boardManager = new BoardManager();

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Input.UpdateInput();
    }

    void InitManagers()
    {
        Grid.MakeGrid(10, 10);
        Spawn.Init();
        Board.Init();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Board.Clear();
        InitManagers();
    }

    void OnDestroy()
    {
        if (_instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Board.Clear();
        }
    }
}
