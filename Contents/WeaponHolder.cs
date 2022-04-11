using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Collider[] _colliders;
    [SerializeField] private Collider[] _unarmColliders;
    private bool _currentActive = true;

    public void CheckColliders(bool otherHand, BattleHeroController controller)
    {
        _colliders = GetComponentsInChildren<Collider>();

        if (_colliders.Length == 0 && otherHand == false)
        {
            _colliders = _unarmColliders;
        }
        else
        {
            int unarmSize = _unarmColliders.Length;
            for (int i = 0; i < unarmSize; i++)
                _unarmColliders[i].enabled = false;
        }

        int size = _colliders.Length;
        WeaponAttack attack = _colliders[0].gameObject.AddComponent<WeaponAttack>();
        attack.SetHeroController(controller);

        for (int i = 0; i < size; i++)
        {
            _colliders[i].isTrigger = true;
        }
        SetActive(false);
    }

    public void SetActive(bool active)
    {
        if (_currentActive == active)
            return;

        _currentActive = active;

        int size = _colliders.Length;
        for (int i = 0; i < size; i++)
            _colliders[i].enabled = active;
    }

    public bool HasWeaponCollider()
    {
        return _colliders != null;
    }

}
