using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float totalHouses = 0;

    float score = 0;

    long finalScore;

    [SerializeField] HighScoreTable gameOverScreen;
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerCamera;
    [SerializeField] GameObject playerMinimapCamera;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

        }
        else
        {
            Debug.Log("More than one game manager");
            Destroy(this);
        }
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        score += Time.deltaTime;
    }

    public void AddHouse()
    {
        totalHouses++;
    }

    public void BurnHouse() 
    {
        totalHouses--;
        if(totalHouses <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        finalScore = (long)score;
        Destroy(playerCamera);
        Destroy(playerMinimapCamera);   
        Destroy(player);
        gameOverScreen.GameOver(finalScore);
    }

    public void AddScore(float _score)
    {
        score += score;
    }

    public void HighScoreEntry(string name)
    {
        gameOverScreen.AddHighScoreEntry(finalScore, name);
    }

}
