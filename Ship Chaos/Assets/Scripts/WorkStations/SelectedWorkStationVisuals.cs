using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectedWorkStationVisuals : MonoBehaviour
{
    [FormerlySerializedAs("_baseCounter")] [SerializeField] private BaseWorkStation baseWorkStation;
    [SerializeField] private GameObject[] _visuals;

    private void Awake()
    {
        Hide();
    }

    private void Start()
    {
        Player.SelectedCounterChanged += HandleSelectedCounterChanged;
    }

    private void HandleSelectedCounterChanged(BaseWorkStation obj)
    {
        if (obj == baseWorkStation)
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
