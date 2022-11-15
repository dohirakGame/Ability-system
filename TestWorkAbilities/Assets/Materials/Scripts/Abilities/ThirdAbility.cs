using System.Collections;
using UnityEngine;

namespace Materials.Scripts.Abilities
{
    public class ThirdAbility : Ability
    {
        private Vector3 _startSize;
        
        [SerializeField] private AbilityParameters _parameters;
        public AbilityParameters abilityParameters
        {
            get {return _parameters; }
            set {_parameters = value; }
        }

        public override void AbilityUse()
        {
            StartAbility();
            StartCoroutine(AbilityDuration());
        }

        private void StartAbility()
        {
            _startSize = gameObject.transform.localScale;
        }

        private void PlusSuze()
        {
            gameObject.transform.localScale *= abilityParameters.Strength;
            gameObject.transform.localPosition =
                new Vector3(gameObject.transform.position.x, transform.position.y + 3f);
        }

        private void SetNormalSize()
        {
            gameObject.transform.localScale = _startSize;
            gameObject.transform.localPosition =
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 2f);
        }

        private IEnumerator AbilityDuration()
        {
            PlusSuze();
            yield return new WaitForSeconds(abilityParameters.Duration);
            SetNormalSize();
        }
    }
}
