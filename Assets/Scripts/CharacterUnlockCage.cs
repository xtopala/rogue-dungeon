using UnityEngine;

public class CharacterUnlockCage : MonoBehaviour
{
    public GameObject message;

    public CharacterSelector[] charSelects;
    private CharacterSelector playerToUnlock;

    public SpriteRenderer cagedSR;

    private bool canUnlock;

    // Start is called before the first frame update
    void Start()
    {

        playerToUnlock = charSelects[Random.Range(0, charSelects.Length)];
        cagedSR.sprite = playerToUnlock.playerToSpawn.bodySR.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (canUnlock)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Instantiate(playerToUnlock, transform.position, transform.rotation);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canUnlock = true;
            message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canUnlock = false;
            message.SetActive(false);
        }

    }
}
