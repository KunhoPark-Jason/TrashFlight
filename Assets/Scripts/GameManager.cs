using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// 싱글턴...? Gamemanager 하는 것이라 함... 뭐지?
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private int coin = 0;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private GameObject gameClearPanel;

    [HideInInspector]
    public bool isGameOver = false;
    
    [HideInInspector]
    public bool isGameClear = false;

    void Awake() {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void IncreaseCoin()
    {
        coin ++;
        text.SetText(coin.ToString());

        if (coin % 30 == 0)
        {
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.Upgrade();
            }
        }
    }

    public void SetGameOver()
    {
        isGameOver = true;

        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if (enemySpawner != null)
        {
            enemySpawner.StopEnemyRoutine();
        }

        Invoke("ShowGameOverPanel", 1f);
    }

    public void SetGameClear()
    {
        isGameClear = true;

        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if (enemySpawner != null)
        {
            enemySpawner.StopEnemyRoutine();
        }

        Invoke("ShowGameClearPanel", 1f);
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    void ShowGameClearPanel()
    {
        gameClearPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        // 1) 버튼 클릭 시 소리를 재생
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        // 2) 일정 시간 후에 씬을 다시 로드
        Invoke("ReloadScene", 0.3f); // 0.5초 뒤에 ReloadScene() 실행
    }

    void ReloadScene()
    {
        SceneManager.LoadScene("TrashFlight");
    }
}
