using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public GameObject buyMessage;

    private bool inBuyZone;

    public bool isHealthRestore, isHealtUpgrade, isWeapon;

    public int itemCost;

    public int healthUpgradeAmount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inBuyZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (LevelManager.instance.currentCoins >= itemCost)
                {
                    LevelManager.instance.SpendCoins(itemCost);

                    if (isHealthRestore)
                    {
                        PlayerHealthController.instance.HealPlayer(PlayerHealthController.instance.maxHealth);
                    }

                    if (isHealtUpgrade)
                    {
                        PlayerHealthController.instance.IncreaseMaxHealth(healthUpgradeAmount);
                    }

                    gameObject.SetActive(false);
                    inBuyZone = false;

                    AudioManager.instance.PlaySFX(18);
                } else
                {
                    AudioManager.instance.PlaySFX(19);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            buyMessage.SetActive(true);
            inBuyZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            buyMessage.SetActive(false);
            inBuyZone = false;
        }
    }
}
