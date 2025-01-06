using UnityEngine;

public class Weapon : MonoBehaviour
{   
    [SerializeField]
    private float moveSpeed = 10f;
    public float damage = 1f;

    private float maxY = 5.5f;
    void Start()
    {

    }
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        if (transform.position.y > maxY){
            Destroy(gameObject);
        }
    }
}
