using BlueRacconGames.MeleeCombat.Implementation;
using BlueRacconGames.Movement;
using System.Collections;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public class SwordMeleeCombatController : MeleeCombatControllerBase
    {
        [SerializeField] private SwordMeleeWeaponFactorySO baseData;

        private bool isDamageDetecting;
        private TopDownCharacterController2D characterController;

        protected override void Awake()
        {
            base.Awake();

            characterController = GetComponent<TopDownCharacterController2D>();

            weapon = baseData.CreateItem() as IMeleeWeapon;
        }

        private void FixedUpdate()
        {
            if(!isDamageDetecting) return;

            DamageDetected();
        }

        public override void Attack()
        {
            if (!CanAttack()) return;

            animationController.AttackAnimation();
            StartCoroutine(AttackStopSequnce());
        }

        public void ActiveDamageDetector()
        {
            isDamageDetecting = true;
        }

        public void DeactiveDamageDetector()
        {
            isDamageDetecting = false;
        }

        protected override bool CanAttack()
        {
            return true;
        }

        private IEnumerator AttackStopSequnce()
        {
            yield return new WaitUntil(() => animationController.GetBoolParameter("IsAttack"));

            characterController.SetCanMove(false);

            yield return new WaitWhile(() => animationController.GetBoolParameter("IsAttack"));

            DeactiveDamageDetector();

            characterController.SetCanMove(true);
        }
    }
}