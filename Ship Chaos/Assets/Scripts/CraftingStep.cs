using System;

[Serializable]
public class CraftingStep
{
    public CraftingStepType type;
    public int amount;
    
    public enum CraftingStepType
    {
        Wait,
        Interact
    }
}