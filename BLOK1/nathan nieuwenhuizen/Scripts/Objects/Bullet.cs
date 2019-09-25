using UnityEngine;

public class Bullet : FloatingEntity
{
    public Character myOwner;

    [SerializeField]
    [Range(0.01f, 0.1f)]
    private float drag = 0.01f;

    private void FixedUpdate()
    {
        rb.velocity *= 1 - drag;
    }

    //disappears when hitting something
    private void OnCollisionEnter2D(Collision2D _collision)
    {
        //Debug.Log(myOwner.gameObject);
        //Debug.Log(_collision.gameObject);
        if (_collision.gameObject != myOwner.gameObject)
        {
            MonoBehaviour[] list = _collision.gameObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour mb in list)
            {
                if (mb is IDestroyable)
                {
                    IDestroyable breakable = (IDestroyable)mb;
                    breakable.TakeDamage(1);
                    //base.Destroy();
                    myOwner.gun.LoadBullet();

                }
            }
        } else
        {
            myOwner.gun.LoadBullet();
        }
    }
}
