using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float totalHouses = 0;

    float score = 0;

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject player;


    private void Start()
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
        Destroy(player);
        gameOverScreen.SetActive(true); 
    }

    public void AddScore(float _score)
    {
        score += score;
    }

}
