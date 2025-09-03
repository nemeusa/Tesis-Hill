using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyGenerator : MonoBehaviour
{
    public int currentEnergy = 0;
    public int maxEnergy = 100;
    public TMP_Text energyText;
    public Animation animTurbine;
    public AnimationClip clipVenti;

    private void Update()
    {
        if(currentEnergy > 1)
        animTurbine.Play();
        //animTurbine.clip = clipVenti;
    }

    public void AddEnergy(int amount)
    {
        currentEnergy += amount;
        if (currentEnergy > maxEnergy)
            currentEnergy = maxEnergy;

        Debug.Log("Energia del generador: " + currentEnergy);
        energyText.text = "Energia: " + currentEnergy + "%";
    }
}
