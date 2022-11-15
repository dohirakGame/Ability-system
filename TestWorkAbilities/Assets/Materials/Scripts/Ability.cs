using UnityEngine;

namespace Materials.Scripts
{
    public abstract class Ability : MonoBehaviour
    {
        public virtual AbilityParameters parameter { get; set; }

        public virtual void AbilityUse()
        {
        }
        
        public virtual void AbilityUse(Vector3 _point)
        {
        }

        public virtual void AbilityUse(Vector3 _point, Vector3 _startPosition)
        {
        }
    }
}
