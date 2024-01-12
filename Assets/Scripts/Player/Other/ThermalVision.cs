using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ThermalVision : MonoBehaviour
{
    public float activeDuration = 5f; // Время активации
    public float cooldownDuration = 5f; // Время перезарядки

    private Collider2D coll; // Коллайдер для управления
    private float activeTimer; // Таймер для отсчета времени
    private float cooldownTimer; // Таймер для отсчета времени
    private bool isCooldown; // Состояние перезарядки
    public Image thermalVisionBar;


    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProvideHeat heatObj = collision.GetComponent<ProvideHeat>();
        if (heatObj != null && heatObj.isWarm)
        {
            if (heatObj.transform.childCount > 0)
                heatObj.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ProvideHeat heatObj = collision.GetComponent<ProvideHeat>();
        if (heatObj != null && heatObj.isWarm)
        {
            if (heatObj.transform.childCount > 0)
                heatObj.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            thermalVisionBar.fillAmount = 1 - (cooldownTimer / cooldownDuration);

            if (cooldownTimer <= 0)
            {
                isCooldown = false;
            }
        }
        else if (activeTimer > 0)
        {
            activeTimer -= Time.deltaTime;
            thermalVisionBar.fillAmount = activeTimer / activeDuration;
            
            if (activeTimer <= 0)
            {
                StartCooldown();
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            StartActive();
        }
    }

    private void StartActive()
    {
        coll.enabled = true; // Включить коллайдер
        activeTimer = activeDuration;
        thermalVisionBar.fillAmount = 1;
    }

    private void StartCooldown()
    {
        coll.enabled = false; // Выключить коллайдер
        isCooldown = true;
        cooldownTimer = cooldownDuration;
        thermalVisionBar.fillAmount = 0;
    }
}
