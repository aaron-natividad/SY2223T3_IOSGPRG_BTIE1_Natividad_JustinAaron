using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public Camera mainCamera;

    [Space(10)]
    [SerializeField] private float followSpeed;
    [SerializeField] private float yOffset;

    [Space(10)]
    [SerializeField] private float shakeTime;
    [SerializeField] private float shakeInterval;
    [SerializeField] private float shakeStrength;

    [Space(10)]
    [SerializeField][Range(0, 1)] private float popScale;
    [SerializeField] private float popSpeed;

    [Space(10)]
    [SerializeField][Range(1, 2)] private float zoomOutScale;
    [SerializeField] private float zoomSpeed;

    private Vector3 targetPos;
    private float currentVelocity;
    private float originalCameraScale;

    private void Awake()
    {
        originalCameraScale = mainCamera.orthographicSize;
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        targetPos = new Vector3(0, 0, 0);
        targetPos.x = 0;
        targetPos.y = Mathf.SmoothDamp(transform.position.y, target.transform.position.y + yOffset, ref currentVelocity, followSpeed);
        targetPos.z = transform.position.z;

        transform.position = targetPos;
    }

    public IEnumerator CO_Shake()
    {
        float timer = shakeTime;
        while (timer > 0)
        {
            mainCamera.transform.localPosition = new Vector3(Random.Range(-shakeStrength, shakeStrength),0,mainCamera.transform.localPosition.z);
            timer -= shakeInterval;
            yield return new WaitForSeconds(shakeInterval);
        }
        mainCamera.transform.localPosition = new Vector3(0, 0, mainCamera.transform.localPosition.z);
    }

    public IEnumerator CO_Pop()
    {
        float scale = originalCameraScale * popScale;
        while (scale < originalCameraScale)
        {
            mainCamera.orthographicSize = scale;
            scale += Time.deltaTime * popSpeed;
            yield return new WaitForFixedUpdate();
        }
        mainCamera.orthographicSize = originalCameraScale;
    }

    public IEnumerator CO_ZoomOut()
    {
        float scale = originalCameraScale * zoomOutScale;
        while (mainCamera.orthographicSize < scale)
        {
            mainCamera.orthographicSize += Time.deltaTime * zoomSpeed;
            yield return new WaitForFixedUpdate();
        }
        mainCamera.orthographicSize = scale;
    }

    public IEnumerator CO_ZoomIn()
    {
        while (mainCamera.orthographicSize > originalCameraScale)
        {
            mainCamera.orthographicSize -= Time.deltaTime * zoomSpeed;
            yield return new WaitForFixedUpdate();
        }
        mainCamera.orthographicSize = originalCameraScale;
    }
}
