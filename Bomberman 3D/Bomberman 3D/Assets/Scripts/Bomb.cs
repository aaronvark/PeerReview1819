using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public delegate void CanDeployBomb();

    public event CanDeployBomb _canDeploy;

    public delegate void TimerCallBack();

    public event TimerCallBack _timerCallBack;

    public bool bombDeployCheck = true;

    [SerializeField] private float explodeTime;
    [SerializeField] private float bombTimer;
    [SerializeField] private float raycastLength;

    private Actor actor;
    private RaycastHit[] hit;
    private Vector3[] directions;

    private void Awake()
    {
        _canDeploy = CanDeploy;
        _timerCallBack = ResetTime;
        gameObject.SetActive(false);
    }

    private void Start()
    {
        actor = Actor.FindObjectOfType<Actor>();
        directions = new Vector3[] { Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
        hit = new RaycastHit[directions.Length];
    }

    private void Update()
    {
        Timer();

        if (Timer() < 0)
        {
            Explode();
        }
    }

    private float Timer()
    {
        return bombTimer -= Time.deltaTime;
    }

    public void Deployed()
    {
        Debug.Log("Almost Deployed");
        if(_canDeploy != null)
        {
            _canDeploy.Invoke();
        }
        Debug.Log("Deployed");
    }

    private void Explode()
    {

        for (int i = 0; i < directions.Length; i++)
        {
            if (Physics.Raycast(transform.position, directions[i], out hit[i], raycastLength))
            {
                Debug.DrawRay(transform.position, directions[i], Color.red);

                try
                {
                    hit[i].collider.GetComponent<IDamagable>().Damage();
                }
                catch
                {

                }
            }
        }
        _canDeploy.Invoke();
        _timerCallBack.Invoke();
        gameObject.SetActive(false);
    }

    private void ResetTime()
    {
        bombTimer = 5;
    }

    private void CanDeploy()
    {
        bombDeployCheck = !bombDeployCheck;
        Debug.Log(bombDeployCheck);
    }
}
