using UnityEngine;
using UnboundLib.GameModes;
using System.Collections;

namespace Stands.Effects
{
    class SethanMono : MonoBehaviour
    {
        Player target;
        Player source;
        float tickRate = 0.5f;
        float timer;
        float effectRadius = 6f;
        float effectRadiusSquared;
        SethanEffectMono effect;
        SethanColorMono effectColor;

        public void SetSource(Player _source)
        {
            source = _source;
        }

        void Awake()
        {
            target = GetComponent<Player>();
            effectRadiusSquared = effectRadius * effectRadius;
            effect = gameObject.AddComponent<SethanEffectMono>();
            effectColor = gameObject.AddComponent<SethanColorMono>();

            PlayerManager.instance.AddPlayerDiedAction(OnPlayerDied);
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, (gm) => DoReset());

            var monos = GetComponentsInChildren<SethanEffectMono>();
            Stands.Debug($"[Sethan] Mono count: {monos.Length}");
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
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }

            Vector3 deltaVector = target.transform.position - source.transform.position;
            if (deltaVector.sqrMagnitude <= effectRadiusSquared)
            {
                effectColor.AddColor();

                if (timer <= 0)
                {
                    effect.Tick(target.data.maxHealth);
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
