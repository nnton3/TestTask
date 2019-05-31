using UnityEngine;

public class YRotation : MonoBehaviour
{
    private void Update()
    {
        if (Input.touchCount == 2) {
            RotateObject();
        }
        else ResetRotation();
    }

    private Vector2 f0startPos;
    private Vector2 f1startPos;
    [SerializeField] private float rotationSensitivity = 1;
    void RotateObject()
    {
        var touch0 = Input.GetTouch(0);
        var touch1 = Input.GetTouch(1);

        Vector2 center = touch0.position + (touch1.position - touch0.position) / 2;

        if (f0startPos == Vector2.zero && f1startPos == Vector2.zero)
        {
            f0startPos = touch0.position - center;
            f1startPos = touch1.position - center;
        }

        Vector2 pos0Vector = touch0.position - center;
        Vector2 pos1Vector = touch1.position - center;

        float f0angle = Vector2.SignedAngle(pos0Vector, f0startPos);
        float f1angle = Vector2.SignedAngle(pos1Vector, f1startPos);
        //Debug.Log("Поворот по Y на  " + Mathf.Abs((f1angle + f0angle) / 2));
        if (IsItRotate(f0angle, f1angle))
        {
            float rotY = Mathf.Lerp(transform.eulerAngles.y, transform.eulerAngles.y + rotationSensitivity * Mathf.Abs((f1angle + f0angle) / 2) * Mathf.Sign(-f1angle), Time.deltaTime);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, rotY, transform.eulerAngles.z);
        }

        f0startPos = pos0Vector;
        f1startPos = pos1Vector;
    }

    [SerializeField] private float rotateDeadZone = 0.1f;
    private bool IsItRotate(float firstAngle, float secondAngle)
    {
        return Mathf.Sign(firstAngle) == Mathf.Sign(secondAngle) && Mathf.Abs((secondAngle + firstAngle) / 2) > rotateDeadZone;
    }

    private void ResetRotation()
    {
        f0startPos = Vector3.zero;
        f1startPos = Vector3.zero;
    }
}
