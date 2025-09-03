using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    public int energy = 100;
    public float transferRate = 1;

    public float interactRange = 3f;
    public KeyCode interactKey = KeyCode.E;
    public TMP_Text energyText;

    void Update()
    {
        if (Input.GetKey(interactKey))
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider hit in hits)
            {
                EnergyGenerator generator = hit.GetComponent<EnergyGenerator>();
                if (generator != null)
                {
                    TransferEnergy(generator);
                    break;
                }
            }
        }
        energyText.text =  energy + "%";
    }

    void TransferEnergy(EnergyGenerator generator)
    {
        if (energy > 0)
        {
            int amountToTransfer = Mathf.Min(transferRate * Time.deltaTime > 1 ? Mathf.FloorToInt(transferRate * Time.deltaTime) : 1, energy);
            generator.AddEnergy(amountToTransfer);
            energy -= amountToTransfer;
 
        }
    }
}
