using UnityEngine;

public class GroundedPoint : MonoBehaviour
{
   [SerializeField] private ParticleSystem _groundedEffect;

   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.TryGetComponent(out Player player))
      {
         Instantiate(_groundedEffect, player.transform.position, Quaternion.identity);
      }
   }
}
