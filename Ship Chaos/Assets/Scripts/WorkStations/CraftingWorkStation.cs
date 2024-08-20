using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CraftingWorkStation : BaseWorkStation
{
    [SerializeField] private List<RecipeSO> _recipes;
    [SerializeField] private LabObjectSO _failedAttempt;
    [SerializeField] private List<Transform> _ingredientPositions;

    private List<LabObjectSO> _acceptableLabObjects;
    private List<LabObject> _labObjectsInWorkstation;
    private bool _working;
    private RecipeSO _currentRecipe;
    private int _currentStep;
    private int _progress;
    private float _timer;
    private bool _timerRunning;

    private void Awake()
    {
        _acceptableLabObjects = new List<LabObjectSO>();
        _labObjectsInWorkstation = new List<LabObject>();
        foreach (var recipe in _recipes)
        {
            foreach (var labObject in recipe.input)
            {
                if(!_acceptableLabObjects.Contains(labObject)) _acceptableLabObjects.Add(labObject);
            }
        }
    }

    private void Update()
    {
        if (_timerRunning)
        {
            _timer += Time.deltaTime;
            Debug.Log($"{_timer} / {_currentRecipe.steps[_currentStep].amount}");
            if (_timer >= _currentRecipe.steps[_currentStep].amount)
            {
                _currentStep++;
                _timerRunning = false;
                _timer = 0;

                if (_currentStep >= _currentRecipe.steps.Count)
                {
                    _working = false;
                    foreach (var labObject in _labObjectsInWorkstation)
                    {
                        labObject.DestroyLabObject();
                    }

                    _labObjectsInWorkstation.Clear();
                    
                    LabObject.SpawnLabObject(_currentRecipe.output, this);
                }
            }
        }
    }

    public override void Interact(Player player)
    {
        if (_working) return;
        
        if (player.HasLabObject())
        {
            var labObject = player.GetLabObject();
            if (!_acceptableLabObjects.Contains(labObject.GetLabObjectSo) ||
                _labObjectsInWorkstation.Contains(labObject)) return;
            
            _labObjectsInWorkstation.Add(labObject);
            player.ClearLabObject();
            labObject.SetFollowTransform(_ingredientPositions[_labObjectsInWorkstation.Count]);

        }
        else
        {
            if (!HasLabObject() && _labObjectsInWorkstation.Count != 0)
            {
                _labObjectsInWorkstation[^1].SetLabObjectParent(player);
                _labObjectsInWorkstation.RemoveAt(_labObjectsInWorkstation.Count - 1);
            }

            if (HasLabObject())
            {
                GetLabObject().SetLabObjectParent(player);
                ClearLabObject();
            }
        }
        
    }

    public override void InteractAlternate(Player player)
    {
        if (!_working)
        {
            if(_labObjectsInWorkstation.Count == 0) return;
            
            var recipe = CheckForCorrectRecipe();
            if (recipe == null) // Wrong lab objects in the workstation
            {
                foreach (var labObject in _labObjectsInWorkstation)
                {
                    labObject.DestroyLabObject();
                }

                _labObjectsInWorkstation.Clear();
            
                LabObject.SpawnLabObject(_failedAttempt, this);
            }
            else
            {
                _currentRecipe = recipe;
                _currentStep = 0;
                _progress = 0;
                _timer = 0;
                _working = true;
            }
        }
        else
        {
            switch (_currentRecipe.steps[_currentStep].type)
            {
                case CraftingStep.CraftingStepType.Wait:
                    break;
                case CraftingStep.CraftingStepType.Interact:
                    _progress++;
                    Debug.Log($"Hit! Progress {_progress}/{_currentRecipe.steps[_currentStep].amount}");

                    if (_progress >= _currentRecipe.steps[_currentStep].amount)
                    {
                        _currentStep++;
                        
                        if (_currentStep >= _currentRecipe.steps.Count)
                        {
                            _working = false;
                            foreach (var labObject in _labObjectsInWorkstation)
                            {
                                labObject.DestroyLabObject();
                            }

                            _labObjectsInWorkstation.Clear();

                            LabObject.SpawnLabObject(_currentRecipe.output, this);
                            return;
                        }
                        
                        if (_currentRecipe.steps[_currentStep].type == CraftingStep.CraftingStepType.Wait)
                            _timerRunning = true;
                        _progress = 0;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
            
    }

    private RecipeSO CheckForCorrectRecipe()
    {
        List<LabObjectSO> labObjectSos = new List<LabObjectSO>();
        foreach (var labObject in _labObjectsInWorkstation)
        {
            labObjectSos.Add(labObject.GetLabObjectSo);
        }
        foreach (var recipe in _recipes)
        {
            var correctRecipe = true;
            
            foreach (var input in recipe.input)
            {
                if (!labObjectSos.Contains(input)) correctRecipe = false;
            }

            if (correctRecipe)
            {
                return recipe;
            } 
        }

        return null;
    }
}
