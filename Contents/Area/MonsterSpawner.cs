using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private List<string> _spawnMonsterNameList = new List<string>();
    [SerializeField]
    private int _monsterSpawnTime = 5;
    private int _lastSpawnTime = 0;

    private Transform _transform;

    private void Start()
    {
        Managers.Map.DayChangeUpdate += OnDayChangeUpdate;
        _transform = GetComponent<Transform>();
    }

    private void OnDayChangeUpdate(int day)
    {
        _lastSpawnTime++;
        if(_lastSpawnTime >= _monsterSpawnTime)
        {
            MonsterSpawn();
            _lastSpawnTime = 0;
        }
    }

    private void MonsterSpawn()
    {
        int spawnMonster = Random.Range(0, _spawnMonsterNameList.Count);

        Data.MonsterData data = Managers.Data.MonsterDataDict[_spawnMonsterNameList[spawnMonster]];
        int groupId = Managers.General.MakeGroup(data.Name, 100, 0, _transform.position, Define.GroupType.Monster);

        Data.StatData stat = new Data.StatData();
        stat.MonsterDataInput(data);

        Managers.General.MakeCharacter(groupId, data.Name, stat, Define.NPCPersonality.Offensive, null, null, null);



        //Managers.General.MakeGroup()
    }
}
