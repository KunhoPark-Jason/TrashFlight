using TMPro;
using UnityEngine;

// 싱글턴...? Gamemanager 하는 것이라 함... 뭐지?
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private int coin = 0;

    [SerializeField]
    private TextMeshProUGUI text;

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
}
