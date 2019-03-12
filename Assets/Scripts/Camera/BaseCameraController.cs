using UnityEngine;
using Curveball;
using DG.Tweening;

namespace LastStand
{
    public class BaseCameraController : CBGGameObject
    {
        public Transform Anchor;

        public Vector3 CameraOffset;
        public float MinZoomHeight;
        public float MaxZoomHeight;
        public float MoveSpeed;

        [Space]
        public float ZoomSpeed;
        public float ZoomLerpSpeed;
        private float zoomTo;

        [Space]
        public float RotationLerpTime;
        public Ease RotationEase;
        private float rotateTo;
        private float currentRotation;
        private Tweener rotationTweener;

        private void OnEnable()
        {
            Anchor.position = BaseModel.CurrentBase.Center;
            transform.localPosition = CameraOffset;
            transform.LookAt(Anchor);
            zoomTo = transform.localPosition.y;
            rotateTo = currentRotation = Anchor.rotation.eulerAngles.y;
        }

        private void OnDisable()
        {
            if (rotationTweener == null)
            {
                rotationTweener.Kill();
                rotationTweener = null;
            }
        }

        private void Update()
        {
            UpdateRotation();
            UpdateBaseCameraPosition();
            UpdateCameraZoom();
        }

        void UpdateBaseCameraPosition()
        {
            Vector3 moveVector = transform.position;

            Vector3 forwardVector = Anchor.forward;
            Vector3 rightVector = Anchor.right;

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

            horizontal = Mathf.Clamp(horizontal, -magnitudeVector.x, magnitudeVector.x);
            vertical = Mathf.Clamp(vertical, -magnitudeVector.y, magnitudeVector.y) * -1f;

            Vector3 moveTo = Anchor.position + (forwardVector * vertical) + (rightVector * horizontal);

            Vector3 baseCenter = BaseModel.CurrentBase.Center;
            Vector2 baseBounds = BaseModel.CurrentBase.Bounds;

            moveTo.x = Mathf.Clamp(moveTo.x, baseCenter.x - baseBounds.x, baseCenter.x + baseBounds.x);
            moveTo.z = Mathf.Clamp(moveTo.z, baseCenter.z - baseBounds.y, baseCenter.z + baseBounds.y);
            moveTo.y = 0f;

            Anchor.position = Vector3.Lerp(Anchor.position, moveTo, Time.deltaTime * MoveSpeed);
        }

        void UpdateCameraZoom()
        {
            Vector3 localPosition = transform.localPosition;

            float zoomAmount = Input.GetAxis("Zoom") * ZoomSpeed;
            zoomTo = Mathf.Clamp(zoomTo + zoomAmount, MinZoomHeight, MaxZoomHeight);
            localPosition.y = Mathf.Lerp(localPosition.y, zoomTo, Time.deltaTime * ZoomLerpSpeed);

            transform.localPosition = localPosition;
        }

        void UpdateRotation()
        {
            if (Input.GetButtonDown("Rotate Camera"))
            {
                rotateTo += Input.GetAxis("Rotate Camera") * -90f;
                rotationTweener = DOTween.To(() => { return currentRotation; }, (float v) => {
                    currentRotation = v;
                    Anchor.eulerAngles = new Vector3(0f, currentRotation, 0f);
                }, rotateTo, RotationLerpTime);

                rotationTweener.SetEase(RotationEase);
                rotationTweener.OnComplete(() => { rotationTweener = null; });
            }
            
        }
    }
}