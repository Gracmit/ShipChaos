using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RecipeSO : ScriptableObject
{
    public List<LabObjectSO> input;
    public LabObjectSO output;
    public List<CraftingStep> steps;
}