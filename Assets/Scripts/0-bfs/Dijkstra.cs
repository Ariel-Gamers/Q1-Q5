using System.Collections.Generic;
/**
 * Class that performs the Dijkstra algorithm on the Tilemap Assignment using Dictionaries (2-Set Data structure similar enough to Hashmap),
 * This algorithm is "Dressed" on the enemies to reach the player in the quickest way.
 * Data members are:
 * weights - stores all weights for given node
 * hasVisited - checks if given node was visited by the checked node
 * path - Stores the path choosen by the algorithm.
 * 
 * Functions:
 * FindPath - finds the shortest path using Dijkstra's algorithm.
 * GetPath - returns
 * @author Noy Ohana
 * @since 2021-11
 */
public class Dijkstra
{

    public static Dictionary<NodeType,NodeType> FindPath<NodeType>(WGraph<NodeType> graph, NodeType startNode)
    {
        Dictionary<NodeType, int> weights = new Dictionary<NodeType, int>(); 
        Dictionary<NodeType, bool> hasVisited = new Dictionary<NodeType, bool>(); 
        Dictionary<NodeType, NodeType> path = new Dictionary<NodeType, NodeType>();

        Queue<NodeType> q = new Queue<NodeType>(); //new queue holds a new node
        q.Enqueue(startNode); //add the new node to the queue
        weights.Add(startNode, 0); //add weight of 0 to the actual "distance" traveled.
        while (q.Count != 0) //as long as there are nodes in the queue
        {
            NodeType n = q.Peek(); //check the current node (don't pop!)
            if (!hasVisited.ContainsKey(n)) //see if the node was visited and if not:
            {
                hasVisited.Add(n, true); //set this node as visited
                q.Dequeue(); //pop the current node from the queue

                foreach (var neighbor in graph.Neighbors(n))  //iterate over all the Neighbors of given node
                {
                    if (weights.ContainsKey(neighbor)) //if the weight already set for this node
                    {
                        if (weights[neighbor] > graph.getW(neighbor) + weights[n]) // check if the distance (weight) can be minimized
                        {
                            weights[neighbor] =  graph.getW(neighbor) + weights[n]; //if so then set the lower weight to the given neighbor.
                            path[neighbor] = n;

                        }
                    }
                    else //if the weight is not set then set the weight to the lowest known weight
                    {
                        weights[neighbor] =graph.getW(neighbor) + weights[n] ;
                        path[neighbor] = n;
                    }
                    if (!hasVisited.ContainsKey(neighbor)) //if the neighbour wasn't visited then add the neighbor to visited nodes.
                    {
                        q.Enqueue(neighbor);
                    }
                }
            }
            else
            {
                q.Dequeue(); //free the node and return the path
            }
        }
        return path;

    }

    public static List<NodeType> GetPath<NodeType>(WGraph<NodeType> graph, NodeType startNode, NodeType endNode)
    {
        List<NodeType> path = new List<NodeType>(); //stores the path
        Dictionary<NodeType, NodeType> full_path = FindPath<NodeType>(graph, startNode); //finds the path to the start node
        if (full_path.ContainsKey(endNode))
        {
            NodeType td =endNode; //start the path directions from endNode
            path.Insert(0, td); //insert the first "move" as 0

            while (!path[0].Equals(startNode)) //as long as you did not reach the starting point
            {
                td = full_path[td]; //find the shortest path each step
                path.Insert(0, td); //inset the path to path dictionary
            }
        }
        return path;
    }


}
