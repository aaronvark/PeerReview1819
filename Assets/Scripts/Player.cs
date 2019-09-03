using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region Singleton
    public static Player Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    GameObject cameraObject;

    [Header("Mouse Settings")]
    [SerializeField] float xSensitivity = 0.5f;
    [SerializeField] float ySensitivity = 0.5f;

    [Header("Rotation Settings")]
    [SerializeField] Vector2 maxHorizontalRotation = new Vector2(-120, 120);
    [SerializeField] Vector2 maxVerticalRotation = new Vector2(-80, 45);

    private float yaw = 0f;
    private float pitch = 0f;

    [Space]
    [Header("Gun Settings")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce;
    [SerializeField] int bulletDamage;

    [Space]
    [Tooltip("Spawn positions of bullets (e.g. the position where the bullets come out of the rifle.")]
    [SerializeField] GameObject[] spawnPositions;

    //ObjectPool
    ObjectPoolManager objectPool;

    private void Start()
    {
        objectPool = ObjectPoolManager.Instance;

        cameraObject = GetComponentInChildren<Camera>().gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        DoMovement();

        if (Input.GetButtonDown("Fire1"))
        {
            DoShot();
        }
    }

    void DoMovement()
    {
        
        float _mouseX = Input.GetAxis("Mouse X");
        float _mouseY = Input.GetAxis("Mouse Y");

        yaw += _mouseX * xSensitivity;
        pitch -= _mouseY * ySensitivity;

        yaw = Mathf.Clamp(yaw, maxHorizontalRotation.x, maxHorizontalRotation.y);
        pitch = Mathf.Clamp(pitch, maxVerticalRotation.x, maxVerticalRotation.y);

        cameraObject.transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

    void DoShot()
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            Debug.DrawRay(spawnPositions[i].transform.position, spawnPositions[i].transform.forward, Color.red);
            
            GameObject _bulletClone = objectPool.SpawnFromPool("PlayerBullets", spawnPositions[i].transform.position, spawnPositions[i].transform.rotation);
            _bulletClone.GetComponent<Bullet>().bulletDamage = bulletDamage;

            //_bulletClone.GetComponent<Bullet>().bulletForce = bulletForce;
            _bulletClone.GetComponent<Rigidbody>().AddForce(cameraObject.transform.forward * bulletForce);

        }
    }
}
