using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    public int hitsToBreak;
	public int points = 10;
    public GameObject blockText;
    public List<Color> colors;
    public List<SpriteRenderer> spriteRenderers;
    public int colorIndex = 0;

	void Awake()
    {
		hitsToBreak = Random.Range(1, 4);
        colorIndex = hitsToBreak - 1;
        blockText.GetComponent<TextMesh>().text = hitsToBreak.ToString();
        foreach(SpriteRenderer sr in spriteRenderers)
        {
            Color opaqueColor = new Color(colors[colorIndex].r, colors[colorIndex].g, colors[colorIndex].b, 1f);
            sr.color = opaqueColor;
        }
    }

	public void HitBlock(int damage)
    {
        ScoreManager.Instance.AddPoints(points);
        hitsToBreak -= damage;
        colorIndex -= damage;
        Debug.Log("hitsToBreak: " + hitsToBreak);
        if(hitsToBreak <= 0)
        {
            Debug.Log("Destroying block");
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
