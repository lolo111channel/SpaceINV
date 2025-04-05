using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceInv
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private float _parallaxEffect = 1.0f;
        private Vector3 _lastCameraPos;
        

        private void OnEnable()
        {
            transform.position = new Vector3(0.0f, 0.0f, transform.position.z);
        }


        private void LateUpdate()
        {
            Vector3 movement = _camera.position - _lastCameraPos;

            if (Vector2.Distance(transform.position, _camera.position) > 0.5f)
            {
                transform.position += movement * _parallaxEffect;
            }

            _lastCameraPos = _camera.position;
        }
    }

}
