using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOutfitChecker : MonoBehaviour
{
    [SerializeField]
    private int OutfitType;

    private List<GameObject> _children = new List<GameObject>();

    public delegate void InitEndCheck();
    public InitEndCheck GetChildEnd;

    protected void Start()
    {
        int size = transform.childCount;

        for(int i = 0 ; i < size ; i++)
        {
            _children.Add(transform.GetChild(i).gameObject);
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (GetChildEnd != null)
            GetChildEnd();
    }

    public int GetOutfitType() { return OutfitType; }
    public List<GameObject> GetChildren() { return _children; }
}
