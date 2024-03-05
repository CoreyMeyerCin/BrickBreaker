using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int points = 10;

    public void HitBlock()
    {
        ScoreManager.Instance.AddPoints(points);
        Destroy(gameObject);
    }
}
