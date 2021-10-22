using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (!BossController.instance.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // damage player
            PlayerHealthController.instance.DamagePlayer();
        }

        Destroy(gameObject);
        AudioManager.instance.PlaySFX(4);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
