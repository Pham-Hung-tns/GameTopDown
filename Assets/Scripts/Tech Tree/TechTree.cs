using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TechNode
{
    public Tech tech;
    public List<Tech> requirements;
    public int researchCost;
    public int researchInvested;
    public Vector2 UIposition; // require for GUI placement

    public TechNode(Tech tech, List<Tech> requirements, int researchCost, Vector2 UIposition)
    {
        this.tech = tech;
        this.requirements = requirements;
        this.researchCost = researchCost;
        this.researchInvested = 0;
        this.UIposition = UIposition;
    }
}

[CreateAssetMenu(fileName = "New Tech Tree", menuName = "Tech Tree/New Tech Tree")]
public class TechTree : ScriptableObject
{
    public List<TechNode> tree = new List<TechNode>();

    public bool AddNode(Tech tech, Vector2 UIpos)
    {
        if (tree == null) tree = new List<TechNode>();
        int tIdx = FindTechIndex(tech);
        if(tIdx == -1)
        {
            tree.Add(new TechNode(tech, new List<Tech>(), 0, UIpos));
            return true;
        }
        return false;
    }

    public void DeleteNode(Tech tech)
    {
        int idx = FindTechIndex(tech);
        if (idx == -1) return;
        tree.RemoveAt(idx);
        foreach(TechNode tn in tree)
        {
            if(tn.requirements != null && tn.requirements.Contains(tech))
            {
                tn.requirements.Remove(tech);
            }
        }
    }
    public int FindTechIndex(Tech tech)
    {
        if (tree == null) return -1;
        for(int i = 0; i < tree.Count; i++)
        {
            if(tree[i] != null && tree[i].tech == tech) return i;
        }
        return -1;
    }

    public bool DoesLeadsToInCascade(int query, int subject)
    {
        if (tree == null) return false;
        if (query < 0 || query >= tree.Count) return false;
        if (subject < 0 || subject >= tree.Count) return false;
        if (tree[query].requirements == null) return false;
        foreach(Tech t in tree[query].requirements)
        {
            if (t == null) continue;
            if(t == tree[subject].tech) return true;
            int nextIdx = FindTechIndex(t);
            if (nextIdx == -1) continue;
            if(DoesLeadsToInCascade(nextIdx, subject)) return true;
        }
        return false;
    }

    public bool IsConnectible(int incomingNodeIdx, int outgoingNodeIdx)
    {
        if(incomingNodeIdx == outgoingNodeIdx) return false;
        return !(DoesLeadsToInCascade(incomingNodeIdx,outgoingNodeIdx) || DoesLeadsToInCascade(outgoingNodeIdx,incomingNodeIdx));
    }

    public HashSet<Tech> GetAllPastRequirements(int nodeIdx, bool includeSelfRequirements = true)
    {
        HashSet<Tech> allRequirements = new HashSet<Tech>();
        if (tree == null) return allRequirements;
        if (nodeIdx < 0 || nodeIdx >= tree.Count) return allRequirements;
        if (includeSelfRequirements && tree[nodeIdx].requirements != null)
            allRequirements = new HashSet<Tech>(tree[nodeIdx].requirements);
        if (tree[nodeIdx].requirements == null) return allRequirements;
        foreach(Tech t in tree[nodeIdx].requirements)
        {
            if (t == null) continue;
            int idx = FindTechIndex(t);
            if (idx == -1) continue;
            allRequirements.UnionWith(GetAllPastRequirements(idx));
        }
        return allRequirements;
    }

    public void CorrectRequirementsCascades(int idx)
    {
        HashSet<Tech> allConnectedThroughChildren = GetAllPastRequirements(idx, false);
        foreach(Tech t in allConnectedThroughChildren)
        {
            if(tree[idx].requirements.Contains(t)) tree[idx].requirements.Remove(t);
        }
    }
}
