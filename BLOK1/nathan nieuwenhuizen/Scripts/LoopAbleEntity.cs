using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class makes sure that the object is within the bounds of the world by transporting it to the other side of the screen.
/// It also has a duplicate sprite to make the transition seemeless.
/// </summary>
public class LoopAbleEntity : MonoBehaviour
{
    private Vector2 minBoundaries = new Vector2(-8f, -5);
    private Vector2 maxBoundaries = new Vector2(8f, 5);
    private Vector2 boundSize;
    private Vector2 itemSize;
    private GameObject duplicate;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        boundSize = new Vector2(maxBoundaries.x - minBoundaries.x, maxBoundaries.y - minBoundaries.y);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //size in Units
        itemSize = spriteRenderer.bounds.size;

        duplicate = new GameObject();
        duplicate.AddComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;
        duplicate.GetComponent<SpriteRenderer>().color = spriteRenderer.color;
        //Instantiate(duplicate, transform);
        duplicate.name = "duplicate of " + name;
        duplicate.transform.parent = transform;
        duplicate.transform.localScale = new Vector2(1, 1);
        duplicate.SetActive(false);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        duplicate.SetActive(false);

        Vector2 pos = transform.position;

        if (pos.x > maxBoundaries.x - itemSize.x / 2)
        {
            setDuplicatePos(new Vector2(-boundSize.x, 0));
            if (pos.x > maxBoundaries.x)
            {
                pos.x = minBoundaries.x;
            }
        }
        if (pos.x < minBoundaries.x + itemSize.x / 2)
        {
            setDuplicatePos(new Vector2(boundSize.x, 0));
            if (pos.x < minBoundaries.x)
            {
                pos.x = maxBoundaries.x;
            }
        }

        if (pos.y > maxBoundaries.y - itemSize.y / 2)
        {
            setDuplicatePos(new Vector2(0, -boundSize.y));
            if (pos.y > maxBoundaries.y)
            {
                pos.y = minBoundaries.y;
            }
        }

        if (pos.y < minBoundaries.y + itemSize.y / 2)
        {
            setDuplicatePos(new Vector2(0, boundSize.y));
            if (pos.y < minBoundaries.y)
            {
                pos.y = maxBoundaries.y;
            }
        }

        transform.position = pos;
    }
    private void setDuplicatePos(Vector3 pos)
    {
        duplicate.SetActive(true);
        duplicate.transform.position = transform.position + pos;
    }
}
