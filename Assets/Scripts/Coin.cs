using UnityEngine;

public class Coin : MonoBehaviour
{
    private float minY = -6.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Jump();
    }

    void Jump()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        float randomJumpForce = Random.Range(4f, 8f);
        Vector2 jumpvelocity = Vector2.up * randomJumpForce;
        jumpvelocity.x = Random.Range(-2f, 2f);

        rigidbody.AddForce(jumpvelocity, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY){
            Destroy(gameObject);
        }
    }
}
