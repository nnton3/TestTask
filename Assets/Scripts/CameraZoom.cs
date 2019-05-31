using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private void Update()
    {
        if (Input.touchCount == 2)
        {
            ZoomCamera();
        }
        else
        {
            ResetZoom();
        }
    }

    [SerializeField] private float zoomSensitivity = 1;
    private float previousPositionDifference;
    [SerializeField] private float minY = 3;
    [SerializeField] private float maxY = 30;
    float posY;
    float previusePosY;
    void ZoomCamera()
    {
        float positionDifference = FingerPositionDifference();
        float zoomDirection = -Mathf.Sign(positionDifference);
        float zoomAmp = Mathf.Abs(positionDifference);
        if ((int)previousPositionDifference != (int)positionDifference)
        {
            posY = Mathf.Lerp(transform.position.y, transform.position.y + zoomDirection * zoomSensitivity * zoomAmp, Time.deltaTime);

            if (posY < minY)
            {
                posY = previusePosY;
            }

            if (posY > maxY)
            {
                posY = previusePosY;
            }
            
            previusePosY = posY;
            transform.position = new Vector3(transform.position.x, posY, transform.position.z);

            previousPositionDifference = positionDifference;
        }
    }

    private Vector2 finger0startPos;
    private Vector2 finger1startPos;
    private float FingerPositionDifference()
    {
        var touch0 = Input.GetTouch(0);
        var touch1 = Input.GetTouch(1);

        if (finger0startPos == Vector2.zero && finger1startPos == Vector2.zero)
        {
            finger0startPos = touch0.position;
            finger1startPos = touch1.position;
        }

        Vector2 finger0currentPos = touch0.position;
        Vector2 finger1currentPos = touch1.position;

        return Vector2.Distance(finger0currentPos, finger1currentPos) - Vector2.Distance(finger0startPos, finger1startPos);
    }

    private void ResetZoom()
    {
        finger0startPos = Vector2.zero;
        finger1startPos = Vector2.zero;
    }
}
