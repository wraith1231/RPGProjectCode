using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    private BattleCharacterController _parentController;

    public void SetHeroController(BattleCharacterController controller)
    {
        _parentController = controller;
    }

    private void OnTriggerStay(Collider other)
    {
        BattleCharacterController controller = other.GetComponent<BattleCharacterController>();

        if (controller == null)
        {
            if(other.tag == "Weapon")
            {
                controller = other.GetComponentInParent<BattleCharacterController>();

                _parentController.GetParried();
                controller.GetParried();
            }

            return;
        }

        if(controller.Group != _parentController.Group)
        {
            controller.HitByOther(_parentController, transform.position);
        }
    }
}
