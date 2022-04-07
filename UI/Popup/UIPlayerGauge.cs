using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerGauge : UIPopup
{
    enum Sliders
    {
        HealthBar,
        StaminaBar,
    }

    private PlayerHeroController _player = null;
    public void SetPlayer(PlayerHeroController player)
    {
        _player = player;
    }

    void Start()
    {
        base.Init();

        Bind<Slider>(typeof(Sliders));
    }

    private void Update()
    {
        if(_player != null)
        {
            Get<Slider>((int)Sliders.HealthBar).value = _player.BattleData.CurrentHPPercent;
            Get<Slider>((int)Sliders.StaminaBar).value = _player.BattleData.CurrentSPPercent;
        }

    }
}
