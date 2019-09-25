using System.Collections;
using UnityEngine;

/// <summary>
/// Handles the shooting of the bullet and where it goes.
/// </summary>
public class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float pullSpeed = 5f;

    private Character myOwner;

    [SerializeField]
    private Camera camera;

    private Bullet cBullet;

    private bool retrieve = false;

    private void Start()
    {
        myOwner = GetComponent<Character>();
        PoolManager.instance.CreatePool(bulletPrefab, 1);
    }

    private void Shoot() {
        cBullet = PoolManager.instance.ReuseObject(bulletPrefab, transform.position, ForwardRotationToMouse()).GetComponent<Bullet>();
        cBullet.myOwner = myOwner;
        cBullet.transform.Translate(transform.right * 0.8f);
        cBullet.OnObjectReuse();
        CameraShake.OnShake?.Invoke(0.05f, 5);
    }

    //to get the angle of the bullet right.
    public Quaternion ForwardRotationToMouse() {
        Vector3 dir = Input.mousePosition - camera.WorldToScreenPoint(camera.transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Trigger()
    {
        if (cBullet == null)
        {
            Shoot();
        } else
        {
            retrieve = true;
            RetrieveBullet();
        }
    }
    public void LoadBullet()
    {
        cBullet.Destroy();
        cBullet = null;
        retrieve = false;
    }

    public void TriggerHold()
    {
        if (retrieve)
        {
            RetrieveBullet();
        }
    }

    public void Untrigger()
    {
        retrieve = false;
    }

    public void RetrieveBullet()
    {
        Vector2 delta = myOwner.transform.position - cBullet.transform.position;
        Vector2 direction = Vector3.Normalize(delta);
        cBullet.RigidBody.AddForce(direction * pullSpeed);

    }
}
