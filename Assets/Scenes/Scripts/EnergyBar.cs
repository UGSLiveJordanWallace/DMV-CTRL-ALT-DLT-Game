using UnityEngine;
public class EnergyBar
{
    //constants
    public const float START_DISTANCE_FROM_OBJECTIVE = 100;
    public const float START_TIME = 0;
    public const float MAX_ENERGY = 100;
    public const float MAX_ENERGY_DEPLETION_RATE = 10;
    //variables
    private float distanceFromObjective = START_DISTANCE_FROM_OBJECTIVE;
    private float time = START_TIME;
    private float energy = MAX_ENERGY;
    private float energyDepletionRate = MAX_ENERGY_DEPLETION_RATE;
    
    public void update()
    {
        time++;
        if(energyDepletionRate < MAX_ENERGY_DEPLETION_RATE)
        {
            energy = energy - energyDepletionRate;
        }
    }

    public bool energyIsDepleted()
    {
        return energy <= 0;
    }


}