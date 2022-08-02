using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGroupName : UIPopup
{
    enum Texts
    {
        Name,
    }

    private Transform _transform;
    private Transform _camPos;

    public override void Init()
    {
        base.Init();

        Bind<TMP_Text>(typeof(Texts));
        _transform = GetComponent<Transform>();
        _camPos = Camera.main.transform;
    }

    public void ChangeName(int number, Vector3 hitPoint)
    {
        string name = Managers.General.GlobalGroups[number].GroupName;
        name += Managers.General.GlobalGroups[number].GroupPower;
        ChangeName(name, hitPoint);
    }
    public void ChangeName(string name, Vector3 hitPoint)
    {
        Get<TMP_Text>((int)Texts.Name).text = name;
        hitPoint.z += 5f;
        hitPoint.y = _camPos.position.y - 5f;
        _transform.position = hitPoint;
        LookThis();
    }

    private void LookThis()
    {
        _transform.LookAt(_transform.position + _camPos.rotation * Vector3.forward,
               Vector3.up);
    }

    public void TheresNoGroup()
    {
        _transform.position = new Vector3(-500, -500, -500);
    }

}
