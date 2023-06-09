﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnboundLib;
using System.Collections;
using UnboundLib.GameModes;

namespace Stands.Effects
{
    class GratefulDeadMono : MonoBehaviour
    {
        Player target;
        Player source;
        float tickRate = 0.5f;
        float timer;
        float effectRadius = 10f;
        float effectRadiusSquared;
        GratefulDeadEffectMono effect;
        GratefulDeadColorMono effectColor;

        public void SetSource(Player _source)
        {
            source = _source;
        }

        void Awake()
        {
            target = GetComponent<Player>();
            effectRadiusSquared = effectRadius * effectRadius;
            effect = gameObject.AddComponent<GratefulDeadEffectMono>();
            effectColor = gameObject.AddComponent<GratefulDeadColorMono>();

            PlayerManager.instance.AddPlayerDiedAction(OnPlayerDied);
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, (gm) => DoReset());

            var monos = GetComponentsInChildren<GratefulDeadEffectMono>();
            Stands.Debug($"[GratefulDead] Mono count: {monos.Length}");
        }

        void OnPlayerDied(Player _player, int _id)
        {
            if (_player == target)
            {
                Reset();
            }
        }

        IEnumerator DoReset()
        {
            Reset();
            yield break;
        }

        void Reset()
        {
            effect.Reset();
            effectColor.Reset();
        }

        void Update()
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }

            Vector3 deltaVector = target.transform.position - source.transform.position;
            if(deltaVector.sqrMagnitude <= effectRadiusSquared)
            {
                effectColor.AddColor();

                if (timer <= 0)
                {
                    effect.Tick();
                    timer = tickRate;
                }
            }
            else
            {
                effectColor.RemoveColor();
            }
        }
    }
}
