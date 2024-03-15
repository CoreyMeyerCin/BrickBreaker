using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNode
{
    public int Depth = 0;
    public GameObject ContainedObject;
    public string DisplayText;
    public TreeNode NodeLeft;
    public TreeNode NodeRight;

    public TreeNode(GameObject containedObject)
    {
        ContainedObject = containedObject;
        DisplayText = GetDisplayText(containedObject);
        NodeLeft = null;
        NodeRight = null;
    }

    private string GetDisplayText(GameObject containedObject)
    {
        var enemy = ContainedObject.GetComponent<EnemyBase>(); //this needs refactored into a common StageObject type instead of just enemy

		if (enemy != null)
        {
            return enemy.DisplayName;
        }

        return containedObject.name;
    }
}
