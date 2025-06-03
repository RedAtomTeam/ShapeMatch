using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SaveInitializator : MonoBehaviour
{
    [SerializeField] private LevelsConfig _levelsConfig;

    void Start()
    {
        for (int i = 0; i < _levelsConfig.levels.Count; i++)
        {
            bool isContains = false;
            for (int j = 0; j < YandexGame.savesData.levels.Count; j++)
            {
                if (_levelsConfig.levels[i].id == YandexGame.savesData.levels[j].id)
                {
                    isContains = true;
                    break;
                }
            }
            if (!isContains)
            {
                YandexGame.savesData.levels.Add(_levelsConfig.levels[i]);
            }

        }
    }
}
