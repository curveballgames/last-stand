using UnityEngine;
using Curveball;
using UnityEngine.Rendering.PostProcessing;

namespace LastStand
{
    public class TrackballCameraController : CBGGameObject
    {
        public Transform Anchor;
        public float MoveSpeed;
        public Vector3 BoundsCenter;
        public Vector2 Bounds;

        [Space]
        public float RotateSpeed;
        public float TrackballRotateSpeed;
        public float RotationLerpSpeed;

        [Space]
        public float MinZoomRadius;
        public float MaxZoomRadius;
        public float ZoomSpeed;

        [Space]
        [Range(5f, 85f)]
        public float MinPitch = 25f;
        [Range(5f, 85f)]
        public float MaxPitch = 85f;

        [Space]
        public PostProcessProfile PostProcessingBehaviour;

        private Timer dragDelayTimer;
        private const float DRAG_DELAY = 0.1f;

        private float zoomRadius;
        private float zoomLerp;

        private Vector3 targetPos;

        private float yawLerp;
        private float yaw;

        private float pitchLerp;
        private float pitch;

        private void OnEnable()
        {
            if (dragDelayTimer == null)
            {
                dragDelayTimer = Timer.CreateTimer(gameObject, DRAG_DELAY, null, null, false, false);
            }

            Anchor.position = Vector3.zero;

            yaw = yawLerp = 180f;
            pitch = pitchLerp = Mathf.Lerp(MinPitch, MaxPitch, 0.5f);
            zoomRadius = zoomLerp = Mathf.Lerp(MinZoomRadius, MaxZoomRadius, 0.5f);

            UpdateLocalPosition(true);
        }

        private void LateUpdate()
        {
            UpdateCameraZoom();
            UpdateCameraRotation();
            UpdateLocalPosition(false);
            UpdateCameraPosition();
        }

        private void UpdateLocalPosition(bool immediate)
        {
            transform.localPosition = Vector3.zero;
            transform.forward = Vector3.forward;
            transform.Rotate(pitchLerp, yawLerp, 0f);

            transform.localPosition -= transform.forward * zoomLerp;
        }

        void UpdateCameraPosition()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float horizontalMagnitude = Mathf.Abs(horizontal);
            float verticalMagnitude = Mathf.Abs(vertical);
            float magnitude = horizontalMagnitude + verticalMagnitude;

            if (magnitude == 0f)
            {
                return;
            }

            Vector2 magnitudeVector = new Vector2(horizontalMagnitude, verticalMagnitude);
            if (magnitudeVector.magnitude > 1f)
            {
                magnitudeVector.Normalize();
            }

            Vector3 forwardVector = transform.forward;
            forwardVector.y = 0f;
            forwardVector.Normalize();

            Vector3 rightVector = transform.right;
            rightVector.y = 0f;
            rightVector.Normalize();

            horizontal = Mathf.Clamp(horizontal, -magnitudeVector.x, magnitudeVector.x);
            vertical = Mathf.Clamp(vertical, -magnitudeVector.y, magnitudeVector.y);

            Vector3 moveTo = Anchor.position + (forwardVector * vertical) + (rightVector * horizontal);

            moveTo.x = Mathf.Clamp(moveTo.x, BoundsCenter.x - Bounds.x, BoundsCenter.x + Bounds.x);
            moveTo.z = Mathf.Clamp(moveTo.z, BoundsCenter.z - Bounds.y, BoundsCenter.z + Bounds.y);
            moveTo.y = 0f;

            Anchor.position = Vector3.Lerp(Anchor.position, moveTo, Time.deltaTime * MoveSpeed);
        }

        void UpdateCameraZoom()
        {
            if (!Mathf.Approximately(Input.GetAxis("Zoom"), 0f))
            {
                float zoomAmount = Input.GetAxis("Zoom") * ZoomSpeed * Time.deltaTime;
                zoomRadius = Mathf.Clamp(zoomRadius + zoomAmount, MinZoomRadius, MaxZoomRadius);
            }

            zoomLerp = Mathf.Lerp(zoomLerp, zoomRadius, Time.deltaTime * RotationLerpSpeed);
            PostProcessingBehaviour.GetSetting<DepthOfField>().focusDistance.value = zoomLerp;
        }

        void UpdateCameraRotation()
        {
            if (Input.GetButton("Trackball Button"))
            {
                if (Input.GetButtonDown("Trackball Button"))
                {
                    dragDelayTimer.StartTimer(true);
                }

                if (!dragDelayTimer.IsTiming)
                {
                    float speedMod = Time.deltaTime * TrackballRotateSpeed;

                    yaw += Input.GetAxis("Trackball Yaw") * speedMod;
                    pitch += Input.GetAxis("Trackball Pitch") * speedMod;
                    pitch = Mathf.Clamp(pitch, MinPitch, MaxPitch);
                }
            }
            else if (Input.GetButton("Rotate Camera"))
            {
                float rotation = Input.GetAxis("Rotate Camera") * Time.deltaTime * RotateSpeed;
                yaw -= rotation;
            }

            float lerpSpeed = Time.deltaTime * RotationLerpSpeed;
            yawLerp = Mathf.Lerp(yawLerp, yaw, lerpSpeed);
            pitchLerp = Mathf.Lerp(pitchLerp, pitch, lerpSpeed);
        }
    }
}