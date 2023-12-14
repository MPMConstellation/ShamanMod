﻿using EntityStates;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace ShamanMod.SkillStates
{
        public class FeralCallScepter : BaseSkillState
    {
        public static float duration = 1f;

        public override void OnEnter()
        {
            base.OnEnter();

            Util.PlaySound("ShamanFrenzyCast", base.gameObject);
            EffectManager.SimpleMuzzleFlash(Modules.Assets.magicImpact2Effect, base.gameObject, "Muzzle", false);

            var literallyeverything = Resources.FindObjectsOfTypeAll(typeof(CharacterBody));

            foreach (CharacterBody cb in literallyeverything as CharacterBody[])
            {
                if (cb.gameObject.GetComponent<TeamComponent>().teamIndex == base.gameObject.GetComponent<TeamComponent>().teamIndex && cb != base.characterBody)
                {
                     if (NetworkServer.active)
                     {
                         cb.AddTimedBuff(Modules.Buffs.acolyteFrenzyBuff, 15f);
                     }
                     Util.PlaySound("ShamanAcolyteFrenziedGrowl", cb.gameObject);
                     EffectManager.SimpleImpactEffect(Modules.Assets.acolyteSummonEffect, cb.gameObject.transform.position, Vector3.up, true);
                }
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.fixedAge >= FeralCall.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }
    }
}