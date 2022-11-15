using System.Collections;
using UnityEngine;

namespace Materials.Scripts.Abilities
{
    public class SecondAbility : Ability
    {
        private PlayerMovementController _playerMovementController;
        private float _startWalkSpeed;
        private float _startSprintSpeed;

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
            _playerMovementController = gameObject.GetComponent<PlayerMovementController>();
            
            _startWalkSpeed = _playerMovementController.walkSpeed;
            _startSprintSpeed = _playerMovementController.sprintSpeed;
        }

        private void PlusSpeed()
        {
            _playerMovementController.walkSpeed *= abilityParameters.Strength;
            _playerMovementController.sprintSpeed *= abilityParameters.Strength;
        }

        private void SetNormalSpeed()
        {
            _playerMovementController.walkSpeed = _startWalkSpeed;
            _playerMovementController.sprintSpeed = _startSprintSpeed;
        }

        private IEnumerator AbilityDuration()
        {
            PlusSpeed();
            yield return new WaitForSeconds(abilityParameters.Duration);
            SetNormalSpeed();
        }
    }
}
