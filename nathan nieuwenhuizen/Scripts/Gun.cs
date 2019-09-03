using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    public void Shoot() {
        Bullet bullet = GameObject.Instantiate(bulletPrefab).GetComponent<Bullet>();
        bullet.gameObject.transform.position = transform.position;
        bullet.gameObject.transform.rotation = ForwardRotationToMouse();
    }

    //to get the angle of the bullet right.
    public Quaternion ForwardRotationToMouse() {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
