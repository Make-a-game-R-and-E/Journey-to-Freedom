using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    [SerializeField] int totalBreadNeeded = 3;
    [SerializeField] int currentBreadCount = 0;

    [Header("Start/Goal Area")]
    [SerializeField] Collider2D startArea;

    [Header("Scene To Load")]
    [Tooltip("The name of the scene you want to load")]
    [SerializeField] string levelNameWin;
    [Tooltip("The name of the scene you want to load")]
    [SerializeField] string levelNameLose;

    private void Awake()
    {
        // If an instance already exists and it's not this one, destroy this object
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance to this one
        Instance = this;

        // Make sure this GameObject isn't destroyed when loading new scenes
        DontDestroyOnLoad(gameObject);
    }

    public void CollectItem(string itemType)
    {
        if (itemType == "Bread")
        {
            currentBreadCount++;
            Debug.Log("Bread collected! Current count: " + currentBreadCount);
        }
    }

    private void Update()
    {
        // Check if Player is in start area with all bread
        if (currentBreadCount >= totalBreadNeeded)
        {
            // If player returns to start area, trigger game win
            // This can be handled in OnTriggerEnter2D in start area if preferred
        }
    }

    public void CheckWinCondition(Collider2D playerCollider)
    {
        if (currentBreadCount >= totalBreadNeeded)
        {
            Debug.Log("You Win! All bread returned to start area.");
            // Load win scene
            SceneManager.LoadScene(levelNameWin);
        }
    }

    public void GameOver()
    {
        ResetBreadCount();
        SceneManager.LoadScene(levelNameLose);
    }

    public void ResetBreadCount()
    {
        currentBreadCount = 0;
    }
}
