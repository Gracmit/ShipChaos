using System;
using UnityEngine;

public class WarshipsManager : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private Transform _firstTilePosition;

    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                var tile = Instantiate(_tilePrefab);
                tile.transform.position = _firstTilePosition.position + new Vector3(i, 0, j);
            }
        }
    }
}
