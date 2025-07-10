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
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitManagers();
    }

    void OnDestroy()
    {
        if (_instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
