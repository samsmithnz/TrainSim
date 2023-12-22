using System.Collections;
using UnityEngine;
//using UnityEngine.Experimental.Rendering.Universal;

namespace TacticsToolkit
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;

        public float size = 10.0f;

        public float smoothing = 5.0f;

        public float minSize = 2.0f;

        public float maxSize = 20.0f;

        public float rotationSpeed = 90.0f;

        public float zoomSpeed = 4.0f;

        private Vector3 desiredPosition;

        public int movementSpeed = 24;

        public float smoothTime = 0.2f;

        private bool isRotating;
        private Quaternion desiredRotation;

        private void Start()
        {
            // Set the desired position and rotation to the current values
            desiredPosition = transform.position;
            desiredRotation = transform.rotation;
        }

        private void Update()
        {
            UpdateZoom();
            MoveCamera();

            if (Input.GetKeyDown(KeyCode.E) && !isRotating)
            {
                StartCoroutine(StartRotation(1));
            }
            if (Input.GetKeyDown(KeyCode.Q) && !isRotating)
            {
                StartCoroutine(StartRotation(-1));
            }
        }

        private void MoveCamera()
        {
            float horizontalInput = Input.GetAxis("Horizontal"); // A & D keys
            float verticalInput = Input.GetAxis("Vertical");     // W & S keys

            //Get camera's front
            Vector3 cameraForward = Camera.main.transform.forward;

            //ignore Y becasue we don't care about the Y angle of the camera. 
            cameraForward.y = 0f;
            cameraForward.Normalize();

            //get camera's right
            Vector3 cameraRight = Camera.main.transform.right;
            cameraForward.y = 0f;
            cameraForward.Normalize();

            // Move the camera
            if (verticalInput > 0)
                transform.Translate(cameraForward * movementSpeed * Time.deltaTime, Space.World);
            if (verticalInput < 0)
                transform.Translate(-cameraForward * movementSpeed * Time.deltaTime, Space.World);

            if (horizontalInput > 0)
                transform.Translate(cameraRight * movementSpeed * Time.deltaTime, Space.World);
            if (horizontalInput < 0)
                transform.Translate(-cameraRight * movementSpeed * Time.deltaTime, Space.World);
        }

        private void UpdateRotation()
        {
            // Rotate the camera based on keyboard input
            float rotateX = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            float rotateY = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            desiredRotation *= Quaternion.Euler(-0, rotateX, rotateY);

            // Limit the camera's rotation to 45 degrees around the target
            float x = desiredRotation.eulerAngles.x;
            if (x > 180) x -= 360;
            x = Mathf.Clamp(x, 0, 30);
            desiredRotation = Quaternion.Euler(x, desiredRotation.eulerAngles.y, 0);
            transform.rotation = desiredRotation;
        }

        private IEnumerator StartRotation(int dir)
        {
            isRotating = true;

            // Calculate the target rotation based on the specified direction
            Quaternion targetRotation = Quaternion.Euler(30f, transform.eulerAngles.y + dir * 90f, transform.rotation.z);

            // Duration of the rotation
            float duration = 0.35f;

            // Initial rotation and time elapsed
            Quaternion startRotation = transform.rotation;
            float elapsedTime = 0f;

            // Rotate gradually over time
            while (elapsedTime < duration)
            {
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure the final rotation is exactly the target rotation
            transform.rotation = targetRotation;

            isRotating = false;
        }

        private void UpdateZoom()
        {
            // Zoom the camera in and out based on scroll wheel input
            float zoom = Input.GetAxis("Mouse ScrollWheel");
            if (zoom != 0f)
            {
                size = Mathf.Round(Mathf.Clamp(size - (zoom * zoomSpeed), minSize, maxSize));
                GetComponent<Camera>().orthographicSize = size;
            }
        }

        private IEnumerator MoveToTarget()
        {
            // Calculate the desired position based on the target's position
            desiredPosition = target.position;

            while (Vector3.Distance(transform.position, desiredPosition) > .1f) // A small threshold value
            {
                // Smoothly move the camera to the desired position
                transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothing);

                yield return null;
            }

            // Ensure the camera reaches the exact desired position
            transform.position = desiredPosition;
        }

        // Function to focus the camera on a new target
        public void FocusOnTarget(GameObject newTarget)
        {
            target = newTarget.transform;
            StartCoroutine(MoveToTarget());
        }
    }
}
