﻿/*
DeepDungeon is licensed under a
Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc-sa/4.0/>.

Original work done by zzi, contributions by Omninewb, Freiheit, Kayla D'orden and mastahg
                                                                                 */
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deep.DungeonDefinition.Base;
using Deep.Helpers;
using ff14bot.Directors;
using ff14bot.Managers;
using static Deep.Tasks.Common;

namespace Deep.DungeonDefinition
{
    public class HeavenOnHigh : DeepDungeonDecorator
    {
        private const uint _BeaconOfReturn = 2009506;
        private const uint _BeaconOfPassage = 2009507;
        private const uint _BossExit = 2005809;
        private const uint _LobbyExit = 2009523;
        private const uint _LobbyEntrance = 2009524;
        private const uint _checkPointLevel = 21;
        private const int _sustainingPotion = 23163;

        private readonly uint[] _ignoreEntity =
        {
            _BeaconOfPassage, _BeaconOfReturn, _LobbyEntrance, Mobs.CatThing, Mobs.Inugami, Mobs.Raiun, 377, 7396, 7395
        };

        public HeavenOnHigh(DeepDungeonData deep) : base(deep)
        {
            BossExit = _BossExit;
            OfReturn = _BeaconOfReturn;
            LobbyExit = _LobbyExit;
            OfPassage = _BeaconOfPassage;
            LobbyEntrance = _LobbyEntrance;
            CheckPointLevel = _checkPointLevel;
            SustainingPotion = _sustainingPotion;
        }

        public override uint OfPassage { get; }
        public override uint OfReturn { get; }
        public override uint BossExit { get; }
        public override uint LobbyExit { get; }
        public override uint LobbyEntrance { get; }
        public override uint CheckPointLevel { get; }

        public override Dictionary<uint, uint> WallMapData { get; } = new Dictionary<uint, uint>
        {
            //mapid - wall file
            {770, 770}, //1-10
            {771, 0}, //11-20
            {772, 0}, //21-30
            {773, 0}, //41-50
            {774, 0}, //61-70
            {775, 0}, //81-90
            {782, 0}, //31-40
            {783, 0}, //51-60
            {784, 0}, //71-80
            {785, 0} //91-100
        };

        public override int SustainingPotion { get; }

        public override uint[] GetIgnoreEntity(uint[] baseList)
        {
            return baseList.Concat(_ignoreEntity).ToArray();
        }

        public override async Task<bool> BuffMe()
        {
            if (CombatTargeting.Instance.LastEntities.Count > 4) return await UsePomander(Pomander.Petrification);

            if (DeepDungeonManager.GetInventoryItem(Pomander.Petrification).Count == 3)
                return await UsePomander(Pomander.Petrification);

            return false;
        }

        public override async Task<bool> BuffBoss()
        {
            return await UsePomander(Pomander.Frailty);
        }

        public override async Task<bool> BuffCurrentFloor()
        {
            if (DeepDungeonManager.GetInventoryItem(Pomander.Frailty).Count > 1)
                return await UsePomander(Pomander.Frailty);

            return false;
        }


        public override string GetDDType()
        {
            return "HoH";
        }
    }
}