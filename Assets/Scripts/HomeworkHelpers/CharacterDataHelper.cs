using System;
using Characters;
using Sirenix.OdinInspector;
using UnityEngine;
using CharacterInfo = Characters.CharacterInfo;

namespace HomeworkHelpers
{
    [Serializable]
    public class CharacterDataHelper
    {
        [InfoBox("Для добавления/удаления статов используйте кнопки ниже! Остальные данные можно менять напрямую через модель.", InfoMessageType.Warning)]
        [SerializeReference]
        private CharacterInfo _characterInfo;

        [SerializeReference]
        private PlayerLevel _playerLevel;

        [SerializeReference]
        private UserInfo _userInfo;

        public CharacterInfo CharacterInfo => _characterInfo;

        public PlayerLevel PlayerLevel => _playerLevel;

        public UserInfo UserInfo => _userInfo;

        public CharacterDataHelper(CharacterInfo characterInfo, PlayerLevel playerLevel, UserInfo userInfo)
        {
            _characterInfo = characterInfo;
            _playerLevel = playerLevel;
            _userInfo = userInfo;
        }

        //Модель менять нельзя, статы через инспектор модели добавлять неудобно, поэтому такое решение
        [Button]
        private void AddStat(string statName, int statValue)
        {
            _characterInfo.AddStat(new CharacterStat(statName, statValue));
        }

        //Модель менять нельзя, статы через инспектор модели удалять невозможно, поэтому такое решение
        [Button]
        private void RemoveStatByName(string statName)
        {
            try
            {
                var stat = _characterInfo.GetStat(statName);
                _characterInfo.RemoveStat(stat);
            }
            catch (Exception)
            {
                Debug.LogError($"No such stat with name {statName}");
            }
        }
    }
}
