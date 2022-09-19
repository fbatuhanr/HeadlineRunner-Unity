using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UiController : MonoBehaviour
{
    [SerializeField] private Transform inGameElements;
    [SerializeField] private CollectedWords _collectedWords;
    [SerializeField] private Leaderboard _leaderboard;
    
    private void Start()
    {
        Time.timeScale = 1;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _collectedWords.point += 100;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            GameFinished();
        }
    }


    public void Restart_Button()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }




    public void GameFinished()
    {
        DestroyInGameElements();
        
        _collectedWords.transform.parent = transform; // extract newspaper element from 'in game object' 
        
        _collectedWords.Invoke("NewspaperMaximize", 1f);
        _leaderboard.Invoke("LeaderboardScreen", 1.5f);
    }

    public void DestroyInGameElements()
    {
        inGameElements.DOScale(Vector3.zero, 0.5f)
            .OnComplete(() => Destroy(inGameElements.gameObject));
    }
}
