using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOutfitChecker : MonoBehaviour
{
    [SerializeField]
    private int OutfitType;

    private GameObject[] _children;
    private void Start()
    {
        _children = gameObject.GetComponentsInChildren<GameObject>();
    }

    public GameObject[] GetChildren() { return _children; }
}
