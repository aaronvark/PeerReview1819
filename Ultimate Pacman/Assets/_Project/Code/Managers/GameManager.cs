using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public sealed class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private string nextScene = "";

    public new Collider2D collider { get; private set; }
    public List<Ghost> ghosts = new List<Ghost>();

    public ReadOnlyCollection<Ghost> GetGhosts => new ReadOnlyCollection<Ghost>(ghosts);

    private float duration = 0f;
    private Coroutine ghostsVulnerable = null;

    public float CurrentVulnerabilityDuration => duration;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        CheckPellets();
    }

    public void AddGhost(Ghost ghost)
    {
        ghosts.Add(ghost);
    }

    public void RemoveGhost(Ghost ghost)
    {
        ghosts.Remove(ghost);
    }

    public void SetGhostsVulnerable(float _duration)
    {
        if (ghostsVulnerable != null)
            StopCoroutine(ghostsVulnerable);
        ghostsVulnerable = StartCoroutine(RunSetGhostsVulnerable(_duration));
    }

    // Set the ghosts in their vulnerable state for the running duration
    IEnumerator RunSetGhostsVulnerable(float _duration)
    {
        foreach (var ghost in ghosts)
        {
            ghost.animator.SetTrigger("SetVulnerable");

            if (ghost.stateMachine != null)
                ghost.stateMachine.SwitchState(new FleeState(ghost.transform));
        }

        duration = _duration;
        while (duration > 0f)
        {
            duration -= Time.deltaTime;
            foreach (var ghost in ghosts)
            {
                ghost.animator.SetFloat("VulnerableTime", duration);
            }

            yield return null;
        }

        foreach (var ghost in ghosts)
        {
            ghost.animator.SetFloat("VulnerableTime", 0f);
            if (ghost.stateMachine != null && ghost.stateMachine.currentState is FleeState)
            {
                ghost.stateMachine.SwitchState(ghost.defaultState);
            }
        }

        yield return null;
    }
    public void CheckPellets()
    {
        if (Pellet.PelletCount == 0)
        {
            SceneHandler.Instance.LoadScene(nextScene);
        }
    }
}
