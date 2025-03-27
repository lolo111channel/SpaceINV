using UnityEngine;
using System.Collections.Generic;

namespace SpaceInv
{
    public class PlayerArrow : MonoBehaviour
    {
        public int CurrentIdObject = 0;

        [SerializeField] private GameObject _arrow;
        [SerializeField] private List<Transform> _objectsTheArrowLookingAt = new();


        private void Update()
        {
            if (_arrow != null && CurrentIdObject < _objectsTheArrowLookingAt.Count)
            {
                Transform target = _objectsTheArrowLookingAt[CurrentIdObject];
                if (target == null)
                {
                    return;
                }
                

                Vector3 look = transform.InverseTransformPoint(target.position);
                float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90f;

                
                _arrow.transform.localRotation = Quaternion.Euler(new(0,0,angle));
            }
        }
    }

}
