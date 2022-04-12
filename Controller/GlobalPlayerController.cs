using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPlayerController : GlobalCharacterController
{

    public GlobalPlayerController() : base()
    {
        _data.Player = true;
        _data.HeroId = 0;
    }
}
