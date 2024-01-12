using UnityEngine;

public class ProvideHeat : MonoBehaviour
{
    public bool isWarm;
    public bool warmPlayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (warmPlayer)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<HPManager>().canFreeze = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (warmPlayer)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<HPManager>().canFreeze = true;
            }
        }
    }
}
