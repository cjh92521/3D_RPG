using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCon : MonoBehaviour
{
    [SerializeField] private Transform model;
    [SerializeField] private Transform horizontal;
    [SerializeField] private Transform vertical;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;

    private bool isLock;
    private Quaternion originVertical;
    private void Start()
    {
        InputManager.Instance.cam = this;
        isLock = false;
        originVertical = vertical.rotation;
    }

    private void Update()
    {
        if(!isLock)
            ResetCamRotation();
    }
    public void CamMoveByMouse()
    {
        StartCoroutine(CamRotation());
    }
    public void ResetCamRotation()
    {
        horizontal.localRotation = model.localRotation;
        vertical.localRotation = originVertical;
    }

    IEnumerator CamRotation()
    {
        isLock = true;
        while (Input.GetMouseButton(0))
        {
            float x = Input.GetAxis("Mouse X") * Time.deltaTime;
            float y = Input.GetAxis("Mouse Y") * Time.deltaTime;

            Quaternion hor = horizontal.localRotation;
            hor.y += x * horizontalSpeed;
            horizontal.localRotation = hor;

            Quaternion ver = vertical.localRotation;
            ver.x -= y * verticalSpeed;
            vertical.localRotation = ver;

            yield return null;
        }
        isLock = false;
    }
}
