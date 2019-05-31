using UnityEngine;
using System.Collections;

public class XRotation : MonoBehaviour
{
    private Touch _touch0;
    private Touch _touch1;
    private void Update()
    {
        //Debug.Log(transform.localEulerAngles);
        if (Input.touchCount == 2)
        {
            _touch0 = Input.GetTouch(0);
            _touch1 = Input.GetTouch(1);
            RotateObject();
        }
        else ResetRotation();
    }

    private Vector2 _f0startPos;
    private Vector2 _f1startPos;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    float previuseRotX;
    float rotX;
    [SerializeField] private float rotationSensitivity = 1;
    private void RotateObject()
    {
        if (_f0startPos == Vector2.zero && _f1startPos == Vector2.zero)
        {
            _f0startPos = _touch0.position;
            _f1startPos = _touch1.position;
        }

        Vector2 f0Vector = _touch0.position - _f0startPos;
        Vector2 f1Vector = _touch1.position - _f1startPos;

        float f0vectorAmp = Vector2.Distance(_f0startPos, _touch0.position);
        float f1vectorAmp = Vector2.Distance(_f1startPos, _touch1.position);
        
        if (IsItRotate(f0Vector, f1Vector))
        {
            rotX = Mathf.Lerp(transform.localEulerAngles.x, transform.localEulerAngles.x + rotationSensitivity * f0vectorAmp * Mathf.Sign(-f0Vector.y), Time.deltaTime);

            Debug.Log(rotX);

            if (rotX < maxX && rotX > minX) rotX = previuseRotX; 
            else Debug.Log("Приемлимое значение");

            previuseRotX = rotX;

            transform.localEulerAngles = new Vector3(rotX, transform.localEulerAngles.y, transform.localEulerAngles.z);
            //Debug.Log(transform.localEulerAngles);
        }

        _f0startPos = _touch0.position;
        _f1startPos = _touch1.position;
    }

    private bool IsItRotate(Vector2 f0vector, Vector2 f1vector)
    {
        return Mathf.Sign(Mathf.Abs(f0vector.y) - Mathf.Abs(f0vector.x)) > 0 &&
               Mathf.Sign(Mathf.Abs(f1vector.y) - Mathf.Abs(f1vector.x)) > 0 && 
               Mathf.Sign(f0vector.y) == Mathf.Sign(f1vector.y);
    }

    private void ResetRotation()
    {
        _f0startPos = Vector2.zero;
        _f1startPos = Vector2.zero;
        previuseRotX = 0f;
    }
}
