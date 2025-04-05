using UnityEngine;


namespace SpaceInv
{
    public class CameraTarget : MonoBehaviour
    {
        private Movement _movement;
        float _angle = 0.0f;

        private void OnEnable()
        {
            _movement = GetComponent<Movement>();
        }


        private void Update()
        {
            _angle += Time.deltaTime * 1.0f;

            _movement.Move(Vector2.up);
            _movement.SetRotation(_angle);
        }
    }

}
