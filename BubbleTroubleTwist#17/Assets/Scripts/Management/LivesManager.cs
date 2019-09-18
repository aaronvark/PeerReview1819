using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    /// <summary>
    /// The maximum amount of lives the players can have
    /// </summary>
    public int maxLives = 3;

    /// <summary>
    /// the amount of lives the players have ( lives are shared )
    /// </summary>
    public int lives = 3;

    /// <summary>
    /// list of images to visualize the lives
    /// </summary>
    public List<Image> livesImages;

    private void OnEnable()
    {
        EventManager.OnPlayerHitHandler += UpdatePlayerLives;
        InitLives();
    }

    private void OnDisable()
    {
        EventManager.OnPlayerHitHandler -= UpdatePlayerLives;
    }

    /// <summary>
    /// Initialize lives
    /// </summary>
    private void InitLives()
    {
        if (livesImages.Count < 1) return;
        foreach (Image image in livesImages)
        {
            if (image == null) return;
            image.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Handles and updates the player lives
    /// </summary>
    /// <param name="_amount">amount of lives lost</param>
    public void UpdatePlayerLives(int _amount)
    {
        lives -= _amount;
        if (lives < 1)
        {
            lives = maxLives;
            EventManager.OnGameOverHandler();
        }
        for (int attackTimes = 0; attackTimes <= _amount - 1; attackTimes++)
        {
            if (lives > 0)
            {
                Image lastImage = livesImages?.FindLast(i => i.gameObject.activeSelf);
                if (lastImage == null) return;
                else lastImage.gameObject.SetActive(false);
            }
        }
    }
}
