using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] Material[] _platformMats;

    public Material GetRandomMaterial => _platformMats[Random.Range(0, _platformMats.Length - 1)];
}
