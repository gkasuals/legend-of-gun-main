using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameOver = false;
    public bool isPause = false;
    [Header("Player Health")]
    public int MaxHealth = 100;
    public int Health;
    private float gameTime;
    private float maxGameTime;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Health = MaxHealth;
    }
    private void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}
