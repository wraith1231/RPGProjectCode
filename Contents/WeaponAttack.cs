using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    private BattleHeroController _parentController;

    public void SetHeroController(BattleHeroController controller)
    {
        _parentController = controller;
    }

    private void OnTriggerEnter(Collider other)
    {
        BattleHeroController controller = other.GetComponent<BattleHeroController>();

        if (controller == null)
        {
            if(other.tag == "Weapon")
            {
                controller = other.GetComponentInParent<BattleHeroController>();
                _parentController.Parried = true;
                controller.Parried = true;
            }

            return;
        }

        if(controller.Data.Group != _parentController.Data.Group)
        {
            float dot = Vector3.Dot(controller.transform.forward, _parentController.transform.forward);
            bool isOpposite = dot < -0.7 ? true : false;
            if(controller.State == Define.HeroState.Block && isOpposite == true)
            {
                controller.GetBlocked(_parentController);
                _parentController.Parried = true;
            }
            else
            {
                controller.GetDamaged(_parentController);
            }
        }
    }
}
