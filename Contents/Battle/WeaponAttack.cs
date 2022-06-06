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
            if (controller.IsHero == true)
            {
                if (controller.State == Define.HeroState.Block)
                {

                    float dot = Vector3.Dot(controller.transform.forward, _parentController.transform.forward);
                    bool isOpposite = dot < -0.7 ? true : false;
                    if (controller.State == Define.HeroState.Block && isOpposite == true)
                    {
                        controller.GetBlocked(_parentController, transform.position);
                        _parentController.GetParried();
                    }
                    else
                    {
                        controller.GetDamaged(_parentController, transform.position);
                    }
                }
                else //hero.state != block
                {
                    controller.GetDamaged(_parentController, transform.position);
                }
            }
            else    //controller.ishero == false
            {
                controller.GetDamaged(_parentController, transform.position);
            }
        }
    }
}
