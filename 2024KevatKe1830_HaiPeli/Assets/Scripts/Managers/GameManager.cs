using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState currentState;
    public PlayerController getPlayer{get; set;}

    private void Awake()
    {
       if(Instance == null) 
       {
           Instance = this;
           DontDestroyOnLoad(gameObject);
       }
       else{
          Destroy(gameObject);
       }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGameState(GameState newState){
        currentState = newState;
        if(currentState == GameState.GameOver){
            StartCoroutine(GameOverCooldown());
        }
    }

    IEnumerator GameOverCooldown(){

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ChangeGameState(GameState.GameRunning);

    }
}
