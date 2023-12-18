using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Characters
{
    //Строго класс не судите, делался только ради удобства тестов и чтобы было что забиндить в контейнере
    [Serializable]
    public class CharactersData
    {
        public const string Jaina = "Jaina";
        public const string Garrosh = "Garrosh";
        public const string Uther = "Uther";

        private readonly Sprite[] _avatars;

        [ShowInInspector]
        private Dictionary<string, CharacterData> _characters = new Dictionary<string, CharacterData>();

        public CharactersData(Sprite[] avatars)
        {
            _avatars = avatars;

            CreateCharactersData();
        }

        public CharacterData GetCharacterData(string name)
        {
            return _characters[name];
        }

        private void CreateCharactersData()
        {
            var utherData = new CharacterData(new CharacterInfo(new[]
                {
                    new CharacterStat("Move Speed", 20),
                    new CharacterStat("Stamina", 25),
                    new CharacterStat("Dexterity", 25),
                    new CharacterStat("Intelligence", 55),
                    new CharacterStat("Damage", 50),
                    new CharacterStat("Regeneration", 5),
                }),
                new PlayerLevel(1, 0),
                new UserInfo(Uther, "I'm the best!", _avatars[0]));

            var garroshData = new CharacterData(new CharacterInfo(new[]
                {
                    new CharacterStat("Move Speed", 20),
                    new CharacterStat("Stamina", 30),
                    new CharacterStat("Dexterity", 30),
                    new CharacterStat("Intelligence", 20),
                    new CharacterStat("Damage", 35),
                    new CharacterStat("Regeneration", 10),
                }),
                new PlayerLevel(2, 100),
                new UserInfo(Garrosh, "I'm the strongest!", _avatars[1]));

            var jainaData = new CharacterData(new CharacterInfo(new[]
                {
                    new CharacterStat("Move Speed", 25),
                    new CharacterStat("Stamina", 15),
                    new CharacterStat("Dexterity", 20),
                    new CharacterStat("Intelligence", 80),
                    new CharacterStat("Damage", 55),
                    new CharacterStat("Regeneration", 2),
                }),
                new PlayerLevel(3, 200),
                new UserInfo(Jaina, "I'm the coolest!", _avatars[2]));

            _characters.Add(Uther, utherData);
            _characters.Add(Garrosh, garroshData);
            _characters.Add(Jaina, jainaData);
        }
    }
}
