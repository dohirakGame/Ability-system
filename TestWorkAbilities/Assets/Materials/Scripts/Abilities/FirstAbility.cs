using UnityEngine;

namespace Materials.Scripts.Abilities
{
    public class FirstAbility : Ability
    {
        [SerializeField] private GameObject stonePrefab;

        private GameObject _stone;

        [SerializeField] private AbilityParameters _parameters;
        
        public AbilityParameters abilityParameters
        {
            get {return _parameters; }
            set {_parameters = value; }
        }

        public override void AbilityUse(Vector3 _point, Vector3 _startPosition)
        {
            InstanceStone(_startPosition);
        }

        private void InstanceStone(Vector3 _startPosition)
        {
            _stone = Instantiate(stonePrefab, _startPosition, Quaternion.identity);
        }
    }
}

