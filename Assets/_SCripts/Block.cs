using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum BlockType
{
    Normal,
    Corrupted
}

public class Block : MonoBehaviour
{
	private float hitTimer = 0f;
    private float hitCooldown = 0.1f;
    public int hitsToBreak;
    public int points = 10;

    public GameObject blockText;
    public List<Color> colors;
    public List<SpriteRenderer> spriteRenderers;
    public int colorIndex = 0;

    public BlockType blockType;

    void Awake()
    {
        hitsToBreak = Random.Range(1, colors.Count + 1);
        colorIndex = hitsToBreak - 1;
        blockText.GetComponent<TextMesh>().text = hitsToBreak.ToString();

        foreach (SpriteRenderer sr in spriteRenderers)
        {
            Color opaqueColor = new Color(colors[colorIndex].r, colors[colorIndex].g, colors[colorIndex].b, 1f);
            sr.color = opaqueColor;
        }
        if (blockType == BlockType.Corrupted)
        {
            ScoreManager.Instance.CorruptedBlocks++;
            ScoreManager.Instance.UpdateScoreText();
        }
    }

    void Update()
    {
        if (hitTimer > 0)
        {
            hitTimer -= Time.deltaTime;
        }
    }

    public void HitBlock(int damage)
    {
        if (hitTimer > 0)
        {
            return;
        }

        hitTimer = hitCooldown;
        ScoreManager.Instance.AddPoints(points);

		hitsToBreak -= damage;
		colorIndex -= damage;
		//Debug.Log("hitsToBreak: " + hitsToBreak);

        StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(0.06f, 0.06f));
        if(hitsToBreak <= 0)
        {
            if(blockType == BlockType.Corrupted)
            {
                //Debug.Log("Destroying block");
                ScoreManager.Instance.CorruptedBlocks--;
                //Debug.Log("Corrupted Block Count::" + ScoreManager.Instance.CorruptedBlocks);
            }

			Events.OnBlockDestroyed(this);
			Destroy(gameObject);
		}

        blockText.GetComponent<TextMesh>().text = hitsToBreak.ToString();
        foreach(SpriteRenderer sr in spriteRenderers)
        {
            Color opaqueColor = new Color(colors[colorIndex].r, colors[colorIndex].g, colors[colorIndex].b, 1f);
            sr.color = opaqueColor;
        }

    }
}
