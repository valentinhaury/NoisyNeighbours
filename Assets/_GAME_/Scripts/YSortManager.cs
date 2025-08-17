using System.Collections.Generic;
using UnityEngine;

public class YSortManager : MonoBehaviour
{
    public List<SpriteRenderer> sprites = new List<SpriteRenderer>();

    void Start()
    {
        sprites.AddRange(FindObjectsOfType<SpriteRenderer>());
    }

    void LateUpdate()
    {
        foreach (var sr in sprites)
        {
            sr.sortingOrder = Mathf.RoundToInt(-sr.transform.position.y * 100);
        }
    }

    public void Register(SpriteRenderer sr)
    {
        if (!sprites.Contains(sr))
            sprites.Add(sr);
    }

    public void Deregister(SpriteRenderer sr)
    {
        if (sprites.Contains(sr))
            sprites.Remove(sr);
    }
}