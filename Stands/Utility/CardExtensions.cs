using System;
using System.Collections.Generic;
using System.Linq;
using UnboundLib.Cards;
using UnityEngine;

namespace Stands.Utility
{
    public static class CardExtensions
    {
        public static void AddScriptToGun(this CustomCard _card, Gun _gun, Type _scriptType)
        {
            List<ObjectsToSpawn> spawnList = _gun.objectsToSpawn.ToList();
            spawnList.Add(new ObjectsToSpawn
            {
                AddToProjectile = new GameObject($"STAND_{_card.cardInfo.cardName}_Effect", new Type[]
                {
                    _scriptType
                })
            });
            _gun.objectsToSpawn = spawnList.ToArray();
        }
    }
}
