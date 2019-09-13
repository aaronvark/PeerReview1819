using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBehaviour : MonoBehaviour
{
    public static CameraBehaviour Instance; //PUUR VOOR TESTEN
    public int CurrentType = 1; //PUUR VOOR TESTEN

    [SerializeField] float turnCameraDuration = 0.5f;
    [SerializeField] AnimationCurve turnCameraCurve;
    [Space]
    [SerializeField] float movementSpeed = 10;
    [SerializeField] GameObject pivot;


    private CinemachineVirtualCamera vCam;

    private Coroutine turnCameraRoutine;

    private void Awake()
    {
        Instance = this; //PUUR VOOR TESTEN
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        HandleMovement();

        if (Input.GetKeyDown(KeyCode.Alpha1)) CurrentType = 1; //PUUR VOOR TESTEN
        if (Input.GetKeyDown(KeyCode.Alpha2)) CurrentType = 2; //PUUR VOOR TESTEN
        if (Input.GetKeyDown(KeyCode.Alpha3)) CurrentType = 3; //PUUR VOOR TESTEN

        if (Input.GetKey(KeyCode.Q)) TurnCamera(true);
        if (Input.GetKey(KeyCode.E)) TurnCamera();
    }

    private void HandleMovement()
    {
        Vector3 _followOffset = vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset; //CHANGE TO PIVOT Y AS

        if (Input.GetKey(KeyCode.W)) _followOffset.y += movementSpeed * Time.deltaTime;
        //if (Input.GetKey(KeyCode.A)) _followOffset.x = _followOffset.z += movementSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) _followOffset.y -= movementSpeed * Time.deltaTime;
        //if (Input.GetKey(KeyCode.D)) _followOffset.x = _followOffset.z -= movementSpeed * Time.deltaTime;

        vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = _followOffset;

        if (Input.mouseScrollDelta.y > 0) vCam.m_Lens.OrthographicSize = Mathf.Clamp(vCam.m_Lens.OrthographicSize - 0.5f, 1, 30);
        if (Input.mouseScrollDelta.y < 0) vCam.m_Lens.OrthographicSize = Mathf.Clamp(vCam.m_Lens.OrthographicSize + 0.5f, 1, 30);
    }

    private void TurnCamera(bool _counterClockWise = false)
    {
        if (turnCameraRoutine != null) return;
        turnCameraRoutine = StartCoroutine(ITurnCamera(_counterClockWise));
    }

    private IEnumerator ITurnCamera(bool _counterClockWise = false)
    {
        float _lerpTime = 0;
        Vector3 _startRot = pivot.transform.eulerAngles;
        Vector3 _endPos = pivot.transform.eulerAngles + (_counterClockWise ? Vector3.up : -Vector3.up) * 90;

        while (_lerpTime < 1)
        {
            _lerpTime += Time.deltaTime / turnCameraDuration;

            float _lerpKey = turnCameraCurve.Evaluate(_lerpTime);
            pivot.transform.eulerAngles = Vector3.Lerp(_startRot, _endPos, _lerpKey);
            yield return null;
        }

        turnCameraRoutine = null;
        yield return null;
    }
}
