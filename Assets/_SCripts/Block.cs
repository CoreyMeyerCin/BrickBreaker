using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        hitsToBreak = Random.Range(1, 7);
        colorIndex = hitsToBreak-1;
        blockText.GetComponent<TextMesh>().text = hitsToBreak.ToString();
        foreach(SpriteRenderer sr in spriteRenderers)
        {
            Color opaqueColor = new Color(colors[colorIndex].r, colors[colorIndex].g, colors[colorIndex].b, 1f);
            sr.color = opaqueColor;
        }
    }

    public void HitBlock()
    {
        ScoreManager.Instance.AddPoints(points);
        hitsToBreak--;
        colorIndex--;
        Debug.Log("hitsToBreak: " + hitsToBreak);
        StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(0.06f, 0.06f));
        if(hitsToBreak <= 0){
            Debug.Log("Destroying block");
            ScoreManager.Instance.Blocks--;
            ScoreManager.Instance.UpdateScoreText();
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
