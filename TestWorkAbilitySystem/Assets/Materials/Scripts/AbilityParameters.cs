using UnityEngine;

namespace Materials.Scripts
{
    [CreateAssetMenu(fileName = "Parameters", menuName = "Abilities/Parameters")]
    public class AbilityParameters : ScriptableObject
    {
        [Header("Name")]
        public string Name;

        [Header("Parameters")]
        [SerializeField] private float reloadTime;
        [SerializeField] private float strength;
        [SerializeField] private float duration;
        [SerializeField] private Action action;

        public float ReloadTime => this.reloadTime;
        public float Strength => this.strength;
        public float Duration => this.duration;
        public Action ActionAbility => this.action;
        
        public enum Action
        {
            Active,
            Passive
        }
    }
}
