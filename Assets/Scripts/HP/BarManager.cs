using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    public HPManager healthManager;
    public HPVarsControl varsControl;
    public Slider healthBar; // ������ �� ������ ��������
    public Slider coldnessBar; // ������ �� ������ �����������

    private void Awake()
    {
        healthBar.maxValue = varsControl.maxHp;
        coldnessBar.maxValue = varsControl.maxColdnessLevel;
    }

    private void Update()
    {
        healthBar.value = healthBar.value = Mathf.Lerp(healthBar.value, healthManager.currentHp, Time.deltaTime * 10); // ������� ���������
        coldnessBar.value = coldnessBar.value = Mathf.Lerp(coldnessBar.value, healthManager.currentColdnessLevel, Time.deltaTime * 10); // ������� ���������
    }



}
