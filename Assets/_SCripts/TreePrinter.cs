using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TreePrinter
{
	class TreeNodeInfo
	{
		public TreeNode Node;
		public string Text;
		public int StartPos;
		public int Size { get { return Text.Length; } }
		public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
		public TreeNodeInfo Parent, NodeLeft, NodeRight;
	}


	//#############################################
	//This is great and all, but it is written to print to a console, which I can't figure out how to do in Unity.
	//It may instead need something more unity-centric like this: https://stackoverflow.com/questions/71299789/showcasing-a-binary-tree-in-unity-problom
	//#############################################



	public static void Print(this TreeNode root, string textFormat = "0", int spacing = 1, int topMargin = 2, int leftMargin = 2)
	{
		//if (root == null) return;
		//int rootTop = Console.CursorTop + topMargin;
		//var last = new List<TreeNodeInfo>();
		//var next = root;
		//for (int level = 0; next != null; level++)
		//{
		//	var item = new TreeNodeInfo { Node = next, Text = next.DisplayText };
		//	if (level < last.Count)
		//	{
		//		item.StartPos = last[level].EndPos + spacing;
		//		last[level] = item;
		//	}
		//	else
		//	{
		//		item.StartPos = leftMargin;
		//		last.Add(item);
		//	}
		//	if (level > 0)
		//	{
		//		item.Parent = last[level - 1];
		//		if (next == item.Parent.Node.NodeLeft)
		//		{
		//			item.Parent.NodeLeft = item;
		//			item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos - 1);
		//		}
		//		else
		//		{
		//			item.Parent.NodeRight = item;
		//			item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos + 1);
		//		}
		//	}
		//	next = next.NodeLeft ?? next.NodeRight;
		//	for (; next == null; item = item.Parent)
		//	{
		//		int top = rootTop + 2 * level;
		//		Print(item.Text, top, item.StartPos);
		//		if (item.NodeLeft != null)
		//		{
		//			Print("/", top + 1, item.NodeLeft.EndPos);
		//			Print("_", top, item.NodeLeft.EndPos + 1, item.StartPos);
		//		}
		//		if (item.NodeRight != null)
		//		{
		//			Print("_", top, item.EndPos, item.NodeRight.StartPos - 1);
		//			Print("\\", top + 1, item.NodeRight.StartPos - 1);
		//		}
		//		if (--level < 0) break;
		//		if (item == item.Parent.NodeLeft)
		//		{
		//			item.Parent.StartPos = item.EndPos + 1;
		//			next = item.Parent.Node.NodeRight;
		//		}
		//		else
		//		{
		//			if (item.Parent.NodeLeft == null)
		//				item.Parent.EndPos = item.StartPos - 1;
		//			else
		//				item.Parent.StartPos += (item.StartPos - 1 - item.Parent.EndPos) / 2;
		//		}
		//	}
		//}
		//Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
	}

	private static void Print(string s, int top, int left, int right = -1)
	{
		Console.SetCursorPosition(left, top);
		if (right < 0) right = left + s.Length;
		while (Console.CursorLeft < right) Console.Write(s);
	}


}
