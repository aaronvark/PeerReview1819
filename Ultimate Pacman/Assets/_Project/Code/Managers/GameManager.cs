using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private string nextScene = "";

    public Collider2D collider { get; private set; }
    private List<Ghost> ghosts = new List<Ghost>();

    private float duration = 0f;
    private Coroutine ghostsVulnerable = null;

    public float CurrentVulnerabilityDuration => duration;

    private void Start()
    {
        collider = GetComponentInChildren<Collider2D>();
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

    IEnumerator RunSetGhostsVulnerable(float _duration)
    {
        duration = _duration;

        foreach (var ghost in ghosts)
        {
            ghost.body.Blink(false);
            ghost.SetVulnerable(true);
        }

        while (duration > 0f)
        {
            if (duration < 2f)
            {
                foreach (var ghost in ghosts)
                {
                    if (ghost.state == Ghost.State.Vulnerable)
                        ghost.body.Blink(Mathf.Ceil(duration / .25f) % 2 == 0);
                }
            }

            duration = Mathf.Max(0f, duration - Time.deltaTime);

            yield return null;
        }

        foreach (var ghost in ghosts)
        {
            ghost.SetVulnerable(false);
        }
        ghostsVulnerable = null;

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
