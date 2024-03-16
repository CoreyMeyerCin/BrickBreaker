using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Manager<LevelManager>
{
	public TreeNode StageTree;
	public TreeNode CurrentStage;
	[SerializeField] public int StageTreeGenerationDepth = 5;
	[SerializeField] public List<GameObject> AvailableObjectsForStageTreeGeneration = new List<GameObject>();

	private void OnEnable()
	{
		Events.OnStageComplete += StageComplete;
	}

	private void OnDisable()
	{
		Events.OnStageComplete -= StageComplete;
	}

	void Start()
    {
		var enemies = GameObject.FindGameObjectsWithTag("enemy");
		AvailableObjectsForStageTreeGeneration.AddRange(enemies);
		StageTree = GenerateStageTree();
		StageTree.Print(textFormat: "(0)", spacing: 2); //this is not going to work right now, see notes in TreePrinter.cs

		CurrentStage = StageTree;
	}

	private void StageComplete()
	{
		//select stage
		//CurrentStage = SelectStage(TreeNode)
	}

	private GameObject GetRandomObjectForRootNode()
	{
		return AvailableObjectsForStageTreeGeneration[Random.Range(0, AvailableObjectsForStageTreeGeneration.Count - 1)];
	}

	private GameObject GetRandomObjectForNodeChild(TreeNode currentNode)
	{
		List<GameObject> availableObjects = new List<GameObject>(AvailableObjectsForStageTreeGeneration); //this needs to be a new list, or C# will modify the existing list because it uses a reference instead
		availableObjects.Remove(currentNode.ContainedObject);

		return availableObjects[Random.Range(0, availableObjects.Count - 1)];
	}

	public TreeNode GenerateStageTree()
	{
		//Debug.Log($"Generating stage tree of depth {StageTreeGenerationDepth}...");
		if (StageTreeGenerationDepth == 0)
			return new TreeNode(GetRandomObjectForRootNode());

		Queue<TreeNode> queue = new Queue<TreeNode>();
		TreeNode root = new TreeNode(GetRandomObjectForRootNode());
		int currentDepth = 0;

		queue.Enqueue(root);
		while (queue.Count > 0)
		{
			var currentTreeSize = queue.Count;
			currentDepth++;
			if (currentDepth > StageTreeGenerationDepth)
			{
				break;
			}
			else if (queue.Count > 250)
			{
				throw new System.Exception("Stage generation in infinite loop, aborting");
			}
			else
			{
				for (int i = 0; i < currentTreeSize; i++)
				{
					TreeNode node = queue.Dequeue();
					node.NodeLeft = new TreeNode(GetRandomObjectForNodeChild(node));
					node.NodeRight = new TreeNode(GetRandomObjectForNodeChild(node));

					queue.Enqueue(node.NodeLeft);
					queue.Enqueue(node.NodeRight);
				}
			}
		}

		//Debug.Log("Stage tree generated successfully");
		return root;
	}

}
