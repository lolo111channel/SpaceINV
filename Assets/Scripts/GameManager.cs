using UnityEngine;

namespace SpaceInv
{
    public class GameManager : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(collision.gameObject);
        }
    }

}
