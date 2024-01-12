using UnityEngine;

public class HPVarsControl : MonoBehaviour
{
    public HPManager healthManager;
    public int maxHp;
    public float maxColdnessLevel;
    private void Start()
    {
        PassVars();
    }

    public void PassVars()
    {
        healthManager.currentHp = maxHp;
        healthManager.maxColdnessLevel = maxColdnessLevel;
    }
}
