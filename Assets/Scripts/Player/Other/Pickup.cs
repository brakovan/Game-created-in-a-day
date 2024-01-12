using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float interactionRadius = 5f; // Радиус для проверки
    public LayerMask playerlLayer; // Слой, с которым можно взаимодействовать
    public GameObject[] whatToTurnOn;

    public TextMeshProUGUI tooltipText;
    // Флаг для проверки, была ли подсказка уже показана

    void Update()
    {
        Collider2D playerHitCollider = Physics2D.OverlapCircle(transform.position, interactionRadius, playerlLayer);
        if (playerHitCollider != null)
        {
            tooltipText.text = "Нажми Q";
            tooltipText.gameObject.SetActive(true);

            // Проверка на нажатие клавиши E
            if (Input.GetKeyDown(KeyCode.Q))
            {
                for (int i = 0; i < whatToTurnOn.Length; i++)
                {
                    whatToTurnOn[i].SetActive(true);
                }
                Destroy(gameObject);
            }
        }
        else
            tooltipText.gameObject.SetActive(false);


    }

    // Отображение радиуса взаимодействия в редакторе Unity
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
