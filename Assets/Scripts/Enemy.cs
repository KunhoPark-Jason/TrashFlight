using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;
    
    [SerializeField]
    private float moveSpeed = 3f;

    private float minY = -6.5f;

    [SerializeField]
    private float hp = 1f;

    [SerializeField]
    private float maxhp = 1f;

    [SerializeField]
    HealthBar healthBar;

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    void Start() 
    {
        healthBar.UpdateHealthBar(hp, maxhp);        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < minY){
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Weapon")
        {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;
            healthBar.UpdateHealthBar(hp, maxhp);
            if (hp <= 0)
            {
                Destroy(gameObject);
                Instantiate(coin, transform.position, Quaternion.identity);
            }
            Destroy(other.gameObject);
        }
    }
}
