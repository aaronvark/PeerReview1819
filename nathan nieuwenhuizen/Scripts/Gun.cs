using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    private Character myOwner;

    private void Start()
    {
        myOwner = GetComponent<Character>();
        PoolManager.instance.CreatePool(bulletPrefab, 1);
    }

    public void Shoot() {
        Bullet bullet = PoolManager.instance.ReuseObject(bulletPrefab, transform.position, ForwardRotationToMouse()).GetComponent<Bullet>();
        bullet.myOwner = myOwner;
        bullet.OnObjectReuse();
    }

    //to get the angle of the bullet right.
    public Quaternion ForwardRotationToMouse() {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
