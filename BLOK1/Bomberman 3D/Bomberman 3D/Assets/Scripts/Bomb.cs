using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public delegate void CallBack();

    public event CallBack _canDeploy;
    public event CallBack _timerCallBack;

    public bool bombDeployCheck = true;

    [SerializeField] private float bombTimer;
    [SerializeField] private float explosionTime;
    [SerializeField] private float raycastLength;
    [SerializeField] private GameObject particles;

    private float maxBombTimer;
    private float maxExplosionTime;
    private bool isExploding = false;

    private RaycastHit[] hit;
    private Vector3[] directions;
    private Collider collision;
    private MeshRenderer renderer;
    private AudioSource audioSource;
    private HashSet<IDamagable> _damages = new HashSet<IDamagable>();

    private void Awake()
    {
        _canDeploy = CanDeploy;
        _timerCallBack = ResetTime;
        gameObject.SetActive(false);

        directions = new Vector3[] { Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
        hit = new RaycastHit[directions.Length];
        collision = GetComponent<Collider>();
        renderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        collision.enabled = false;
        maxBombTimer = bombTimer;
        maxExplosionTime = explosionTime;
    }

    private void Update()
    {
        bombTimer = Timer(bombTimer);

        if (isExploding)
        {
            IsExploding();
        }

        if (bombTimer < 0)
        {
            Explode();
        }
        else if (bombTimer < 2.5f)
        {
            collision.enabled = true;
        }  
    }

    private float Timer(float _timer)
    {
        return _timer -= Time.deltaTime;
    }

    public void Deployed()
    {
        if(_canDeploy != null)
        {
            _canDeploy.Invoke();
        }
    }

    private void Explode()
    {
        isExploding = true;
        renderer.enabled = false;
        particles.SetActive(true);
        particles.transform.position = transform.position;
        collision.enabled = false;
    }

    private void IsExploding()
    {
        explosionTime = Timer(explosionTime);

        if (explosionTime > 0)
        {
            for (int i = 0; i < directions.Length; i++)
            {
                if (Physics.Raycast(transform.position, directions[i], out hit[i], raycastLength))
                {
                    Debug.DrawRay(transform.position, directions[i], Color.red);
                    IDamagable temp = hit[i].collider.GetComponent<IDamagable>();
                    if(temp != null)
                    {
                        _damages.Add(temp);
                    }
                }
            }
        }
        else
        {
            foreach (IDamagable item in _damages)
            {
                if (item != null)
                {
                    item.Damage();
                }
            }

            _damages = new HashSet<IDamagable>();
            _canDeploy.Invoke();
            _timerCallBack.Invoke();
            isExploding = false;
            gameObject.SetActive(false);
        }
    }

    private void ResetTime()
    {
        bombTimer = maxBombTimer;
        explosionTime = maxExplosionTime;
        particles.SetActive(false);
        renderer.enabled = true;
    }

    private void CanDeploy()
    {
        bombDeployCheck = !bombDeployCheck;
    }
}
