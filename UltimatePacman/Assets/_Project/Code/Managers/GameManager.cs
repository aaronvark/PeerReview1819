using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField]
    private string nextScene = "You Win";

    private List<Ghost> ghosts = new List<Ghost>();
    private float duration = 0f;
    private Coroutine ghostsVulnerable = null;

    public Collider2D Collider { get; private set; }
    public bool AnyRetreating => ghosts.Exists(ghost => ghost.Animator.GetBool("Retreating"));

    private void Awake()
    {
        Toolbox.Instance.Add(this);
        Collider = GetComponent<Collider2D>();
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
    private IEnumerator RunSetGhostsVulnerable(float _duration)
    {
        if(!AnyRetreating)
            SoundManager.Instance.FleeingSound();

        foreach (var ghost in ghosts)
        {
            ghost.Animator.SetTrigger("SetVulnerable");
        }

        duration = _duration;
        while (duration > 0f)
        {
            duration -= Time.deltaTime;
            foreach (var ghost in ghosts)
            {
                ghost.Animator.SetFloat("VulnerableTime", duration);
            }

            yield return null;
        }

        foreach (var ghost in ghosts)
        {
            ghost.Animator.SetFloat("VulnerableTime", 0f);
        }

        if(!AnyRetreating)
            SoundManager.Instance.ChasingSound();
    }

    public void CheckPellets()
    {
        if (Pellet.PelletCount == 0)
        {
            SoundManager.Instance.StopSound();
            SceneHandler.Instance.LoadScene(nextScene);
        }
    }

    private void OnDestroy()
    {
        Toolbox.Instance.Remove(this);
    }
}
