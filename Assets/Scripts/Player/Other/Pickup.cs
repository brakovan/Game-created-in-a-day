using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float interactionRadius = 5f; // ������ ��� ��������
    public LayerMask playerlLayer; // ����, � ������� ����� �����������������
    public GameObject[] whatToTurnOn;

    public TextMeshProUGUI tooltipText;
    // ���� ��� ��������, ���� �� ��������� ��� ��������

    void Update()
    {
        Collider2D playerHitCollider = Physics2D.OverlapCircle(transform.position, interactionRadius, playerlLayer);
        if (playerHitCollider != null)
        {
            tooltipText.text = "����� Q";
            tooltipText.gameObject.SetActive(true);

            // �������� �� ������� ������� E
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

    // ����������� ������� �������������� � ��������� Unity
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
