using System;
using UnityEngine;

namespace Materials.Scripts.Abilities
{
    [RequireComponent(typeof(Rigidbody))]
    public class StoneTransform : MonoBehaviour
    {
        [Header("Stone parameters")] 
        public float speed;
        public float damage;

        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            StartDirection();
        }

        private void StartDirection()
        {
            _rb.velocity = Vector3.forward * speed;
        }

        private void OnTriggerExit(Collider other)
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.collider.CompareTag("Player") && collision.collider.CompareTag("Ground"))
                Destroy(gameObject);
        }
    }
}
