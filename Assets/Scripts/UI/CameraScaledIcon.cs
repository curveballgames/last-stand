using UnityEngine;
using Curveball;

namespace LastStand
{
    public class CameraScaledIcon : CBGUIComponent
    {
        private const float MAX_DISTANCE = 40f;
        private const float MIN_DISTANCE = 5f;
        private const float DISTANCE_DIVISOR = MAX_DISTANCE - MIN_DISTANCE;
        private const float MIN_SCALE = 0.33f;
        private const float MAX_SCALE = 1f;

        private void LateUpdate()
        {
            float distance = Vector3.Distance(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(RectTransform.position));
            float modifier = Mathf.Clamp(MAX_DISTANCE - distance, 0f, MAX_DISTANCE);
            transform.localScale = Vector3.one * Mathf.Clamp(modifier / DISTANCE_DIVISOR, MIN_SCALE, MAX_SCALE);

            Vector3 position = RectTransform.position;
            position.z = -distance;
            RectTransform.position = position;
        }
    }
}