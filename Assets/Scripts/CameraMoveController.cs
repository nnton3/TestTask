using UnityEngine;
using UnityEngine.UI;

public class CameraMoveController : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (Input.touchCount <= 2 && Input.touchCount > 0)
        {
            MoveCamera();
        }
    }

    [SerializeField] private float moveSensitivity = 0.3f;
    private void MoveCamera()
    {
        var touch = Input.GetTouch(0);
        Vector3 fingerDeltaPos = transform.TransformDirection(new Vector3(-touch.deltaPosition.x, 0, -touch.deltaPosition.y));
        Vector3 localMove = Vector3.Lerp(transform.position, transform.position + fingerDeltaPos * moveSensitivity, Time.deltaTime);
        transform.position = localMove;
    }
}
