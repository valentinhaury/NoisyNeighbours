using System.Collections.Generic;
using UnityEngine;

public class YSortManager : MonoBehaviour
{
    // Klasse für Cache-Einträge
    private class SpriteCache
    {
        public SpriteRenderer spriteRenderer;
        public Vector3 lastPosition;
    }

    private List<SpriteCache> spriteCaches = new List<SpriteCache>();

    void Start()
    {
        // Einmal alle Figuren mit Sorting Layer "figures" finden und cachen
        SpriteRenderer[] allSprites = FindObjectsOfType<SpriteRenderer>();
        foreach (var sr in allSprites)
        {
            if (sr.sortingLayerName == "Player")
            {
                spriteCaches.Add(new SpriteCache
                {
                    spriteRenderer = sr,
                    lastPosition = sr.transform.position
                });
            }
        }
    }

    void LateUpdate()
    {
        foreach (var cache in spriteCaches)
        {
            Vector3 currentPos = cache.spriteRenderer.transform.position;
            if (currentPos != cache.lastPosition)
            {
                // Position hat sich geändert, sortiere neu
                cache.spriteRenderer.sortingOrder = Mathf.RoundToInt(-currentPos.y * 100);
                cache.lastPosition = currentPos;
            }
        }
    }
}

