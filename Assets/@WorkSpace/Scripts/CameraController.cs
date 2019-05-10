using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VACA
{
    public class CameraController : MonoBehaviour
    {
        private GameObject _target;

        public float speed = 1;
        public float amplitude = 2;
        public int octaves = 4;

        private Vector3 destination;
        private Vector3 currentVelocity;
        private int currentTime = 0;

        private void Awake()
        {
            _target = this.gameObject;
        }

        private void FixedUpdate()
        {
            if (!Managers.ContentManager.status.Equals(Managers.ContentManager.Status.Boss))
            {
                return;
            }
            // if number of frames played since last change of direction > octaves create a new destination
            if (currentTime > octaves)
            {
                currentTime = 0;
                destination = generateRandomVector(amplitude);
                //print("new Vector Generated: " + destination);
            }

            // smoothly moves the object to the random destination
            _target.transform.position = Vector3.SmoothDamp(_target.transform.position, destination, ref currentVelocity, speed);

            currentTime++;
        }

        // generates a random vector based on a single amplitude for x y and z
        private Vector3 generateRandomVector(float amp)
        {
            Vector3 result = new Vector3();
            for (int i = 0; i < 3; i++)
            {
                float x = Random.Range(-amp, amp);
                result[i] = x;
            }
            return result;
        }
    }
}