using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    //TODO UI en Events toevoegen.
    #region Singleton
    public static Player Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private GameObject cameraObject;

    [Header("Mouse Settings")]
    [SerializeField] float xSensitivity = 0.5f;
    [SerializeField] float ySensitivity = 0.5f;

    [Header("Rotation Settings")]
    [SerializeField] Vector2 maxHorizontalRotation = new Vector2(-120, 120);
    [SerializeField] Vector2 maxVerticalRotation = new Vector2(-80, 45);

    [Header("Move Settings")]
    [SerializeField] Vector2 movementRestrictions = new Vector2(-120, 120);
    [SerializeField] float moveSpeed;

    private float yaw = 0f;
    private float pitch = 0f;

    [Space]
    [Header("Gun Settings")]
    [SerializeField] Pool bulletPool;
    [SerializeField] float bulletForce;
    [SerializeField] int bulletDamage;

    [Space]
    [Tooltip("Spawn positions of bullets (e.g. the position where the bullets come out of the rifle.")]
    [SerializeField] GameObject[] spawnPositions;

    [Space]
    [Header("Health Settings")]
    [SerializeField] int health;

    //ObjectPool
    private ObjectPoolManager objectPool;

    private void Start()
    {
        objectPool = ObjectPoolManager.Instance;

        if (!objectPool.pools.Contains(bulletPool))
        {
            objectPool.AddPool(bulletPool);
        }

        cameraObject = GetComponentInChildren<Camera>().gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        DoLookAround();
        DoMovement();

        if (Input.GetButtonDown("Fire1"))
        {
            DoShot();
        }
    }

    private void DoMovement()
    {
        float _horizontalAxis = Input.GetAxis("Horizontal");

        Vector3 newPlayerPostion = transform.position;
        newPlayerPostion = new Vector3(transform.position.x + _horizontalAxis * moveSpeed, transform.position.y, transform.position.z);
        newPlayerPostion.x = Mathf.Clamp(newPlayerPostion.x, movementRestrictions.x, movementRestrictions.y);

        transform.position = newPlayerPostion;

        //transform.position = Vector3.Lerp(transform.position, newPlayerPostion, Time.deltaTime * 0.5f);
    }

    private void DoLookAround()
    {
        float _mouseX = Input.GetAxis("Mouse X");
        float _mouseY = Input.GetAxis("Mouse Y");

        yaw += _mouseX * xSensitivity;
        pitch -= _mouseY * ySensitivity;

        yaw = Mathf.Clamp(yaw, maxHorizontalRotation.x, maxHorizontalRotation.y);
        pitch = Mathf.Clamp(pitch, maxVerticalRotation.x, maxVerticalRotation.y);

        cameraObject.transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

    private void DoShot()
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            Debug.DrawRay(spawnPositions[i].transform.position, spawnPositions[i].transform.forward, Color.red);
            
            GameObject _bulletClone = objectPool.SpawnFromPool(bulletPool, spawnPositions[i].transform.position, spawnPositions[i].transform.rotation);
            _bulletClone.GetComponent<Bullet>().bulletDamage = bulletDamage;

            _bulletClone.GetComponent<Rigidbody>().AddForce(cameraObject.transform.forward * bulletForce);

        }
    }

    public void Damage(int damage)
    {

    }

    public void Die()
    {

    }
}
