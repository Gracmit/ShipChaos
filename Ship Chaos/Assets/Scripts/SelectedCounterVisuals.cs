using System;
using UnityEngine;

public class SelectedCounterVisuals : MonoBehaviour
{
    [SerializeField] private BaseCounter _baseCounter;
    [SerializeField] private GameObject[] _visuals;

    private void Awake()
    {
        Hide();
    }

    private void Start()
    {
        Player.SelectedCounterChanged += HandleSelectedCounterChanged;
    }

    private void HandleSelectedCounterChanged(BaseCounter obj)
    {
        if (obj == _baseCounter)
        {
            foreach (var visual in _visuals)
            {
                visual.SetActive(true);
            }
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        foreach (var visual in _visuals)
        {
            visual.SetActive(false);
        }
    }
}
