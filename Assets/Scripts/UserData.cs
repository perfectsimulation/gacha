using System;
using System.Collections.Generic;

[Serializable]
public class UserData
{
    public string username;
    public int playerLevel = 0;
    public ProgressTree progress = new ProgressTree();

    public void Init()
    {
        // TODO
    }

    public static UserData CreateNewInstance()
    {
        UserData userData = new UserData();
        userData.Init();
        return userData;
    }

    public void SetUsername(string username)
    {
        this.username = username;
    }

    public int GetCurrentLevel()
    {
        Node currentNode = this.FindFirstIncompleteNode();
        return currentNode.level;
    }

    public int GetCurrentStage()
    {
        Node currentNode = this.FindFirstIncompleteNode();
        return currentNode.stage;
    }

    // Return all Nodes with the specified level
    public List<Node> GetStagesOfLevel(int level)
    {
        List<Node> stages = new List<Node>();
        foreach (Node n in this.progress.nodes)
        {
            if (n.level == level)
            {
                stages.Add(n);
            }
        }
        return stages;
    }

    private Node FindFirstIncompleteNode()
    {
        // Go through each Node to find the first one with isComplete = false
        foreach (Node n in this.progress.nodes)
        {
            if (n.isComplete == false)
            {
                return n;
            }
        }
        return this.progress.nodes[0];
    }

}

[Serializable]
public class ProgressTree
{
    public List<Node> nodes = new List<Node>();
    public ProgressTree()
    {
        // TODO: construct the real, fresh, new progress tree - for now just placeholder
        Node level0stage7 = new Node(0, 7, new List<string>());
        Node level0stage6 = new Node(0, 6, new List<string>() { level0stage7.id });
        Node level0stage5 = new Node(0, 5, new List<string>() { level0stage6.id });
        Node level0stage4 = new Node(0, 4, new List<string>() { level0stage5.id });
        Node level0stage3 = new Node(0, 3, new List<string>() { level0stage4.id });
        Node level0stage2 = new Node(0, 2, new List<string>() { level0stage3.id });
        Node level0stage1 = new Node(0, 1, new List<string>() { level0stage2.id });
        Node level0stage0 = new Node(0, 0, new List<string>() { level0stage1.id });

        Node level1stage4 = new Node(1, 4, new List<string>());
        Node level1stage3 = new Node(1, 3, new List<string>() { level1stage4.id });
        Node level1stage2 = new Node(1, 2, new List<string>() { level1stage3.id });
        Node level1stage1 = new Node(1, 1, new List<string>() { level1stage2.id });
        Node level1stage0 = new Node(1, 0, new List<string>() { level1stage1.id });

        this.nodes.Add(level0stage0);
        this.nodes.Add(level0stage1);
        this.nodes.Add(level0stage2);
        this.nodes.Add(level0stage3);
        this.nodes.Add(level0stage4);
        this.nodes.Add(level0stage5);
        this.nodes.Add(level0stage6);
        this.nodes.Add(level0stage7);
        this.nodes.Add(level1stage0);
        this.nodes.Add(level1stage1);
        this.nodes.Add(level1stage2);
        this.nodes.Add(level1stage3);
        this.nodes.Add(level1stage4);
    }
}

[Serializable]
public class Node
{
    public int level = 0;
    public int stage = 0;
    public string id;
    public bool isComplete = false;
    public int score = 0;
    public List<string> childrenIDs = new List<string>();
    public Node() { }
    public Node(int level, int stage, List<string> childrenIDs)
    {
        this.level = level;
        this.stage = stage;
        this.id = string.Format("{0}-{1}", this.level, this.stage);
        this.childrenIDs = childrenIDs;
    }
}
