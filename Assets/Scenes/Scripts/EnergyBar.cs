using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar: MonoBehaviour
{
    [SerializeField] private float startingEnergy;
    [SerializeField] private float distanceFromObject;
    [SerializeField] private float maxDepletionRate;

    [SerializeField] private Slider energyBar;
    
    //variables
    private float energy;
    private float energyDepletionRate;

    void Start()
    {
        energy = startingEnergy;
        energyDepletionRate = 0;
    }

    public void Update()
    {
        if(energyDepletionRate < maxDepletionRate)
        {
            energy -= energyDepletionRate;
        }
        
        energyBar.value = energy;

        energyDepletionRate += 0.0001f;
    }

    public bool EnergyIsDepleted()
    {
        return energy <= 0;
    }
}