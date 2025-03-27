using UnityEngine;

namespace SpaceInv
{

    public class AreasThatChangeArrowPoint : MonoBehaviour
    {
        [SerializeField] private int _id = 0;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                Player player = collision.GetComponent<Player>();
                PlayerArrow arrow = player.PlayerArrow;
                arrow.CurrentIdObject = _id;

            }

        }
    }

}

