using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower.Levels
{
    public class PathGenerator
    {
        int[,] grid;
        Node start;
        Node end;

        List<Node> openList;
        List<Node> closeList;
        int rows = 45;
        int cols = 80;

        public PathGenerator(int[,] grid)
        {
            this.grid = grid;
        }

        private bool isInCloseList(Node node)
        {
            foreach(Node n in closeList){
                if (node.isEqual(n))
                {
                    return true;
                }
            }
            return false;
        }
        private bool isInOpenList(Node node)
        {
            foreach (Node n in openList)
            {
                if (node.isEqual(n))
                {
                    return true;
                }
            }
            return false;
        }

        int getFNode()
        {
            Random r = new Random();
            return r.Next(openList.Count);

            //float min = 999999999;
            //// Node* minF_node = nullptr;
            //int index = 0;
            //int i = 0;
            //foreach (Node n in openList)
            //{
            //    if (n.f() < min)
            //    {
            //        min = n.f();
            //        index = i;
            //    }
            //    i++;
            //}
            //return index;
        }

        private void exploreNeightbour(Node node)
        {
            int[] dirRow4 = { 1, -1, 0, 0 };
            int[] dirCol4 = { 0, 0, 1, -1 };
            int border = 3;

            for(int i = 0; i < dirRow4.Length; i++)
            {

                int nextRow = node.row + dirRow4[i];
                int nextCol = node.col + dirCol4[i];

                if (nextRow < border || nextRow > rows - border - 1)
                {
                    //Debug.WriteLine("cc");
                    continue;
                }
                if (nextCol < border || nextCol > cols - border - 1)
                {
                    // Debug.WriteLine("cc");
                    continue;
                }

                Node newNode = new Node(nextRow, nextCol, node);

                if (newNode.isEqual(end))
                {
                    openList.Add(newNode);
                }

                //if (isInOpenList(new Node(nextRow, nextCol))) continue;
                if (isInCloseList(newNode)) continue;
                if (grid[nextRow, nextCol] == 2) continue; // skip plant

               
                if (grid[nextRow, nextCol] == 1) continue; // skip plant


                float path_to_this_neightbour = node.g + 1;

                if (path_to_this_neightbour < newNode.g || !isInOpenList(newNode))
                {
                    newNode.g = path_to_this_neightbour;
                    newNode.h = node.euclidean_distance(newNode,end);
                    if (!isInOpenList(newNode))
                    {
                        openList.Add(newNode);
                    }
                }

            }

        }

        List<Node> tracePath(Node node)
        {
            List<Node> path = new List<Node>();
            path.Add(node);
            Node n = node;
            while (n.parent != null)
            {
                path.Add(n.parent);
                n = n.parent;
            }
            path.Reverse();
            return path;
        }

        public List<Node> getPath(Node start,Node end)

        {
            openList = new List<Node>();
            closeList = new List<Node>();
            this.start = start;
            this.end = end;
            openList.Add(start);


            while (openList.Count != 0)
            {

                Node currentNode = openList[getFNode()];
                closeList.Add(currentNode);
                if (currentNode.isEqual(end))
                {
                    return tracePath(currentNode);
                }

                openList.Remove(currentNode);
                exploreNeightbour(currentNode);
            }

            return new List<Node>();

        }

        public List<Node> getRandomPath()
        {
            Random random = new Random();
            Node path1Dest = new Node(random.Next(3,41), random.Next(3,20));
            List < Node > path1 = new PathGenerator(grid).getPath(new Node(3, 3), path1Dest);
            
            foreach (Node node in path1)
            {
                grid[node.row, node.col] = 1;
            }

            Node path2Dest = new Node(random.Next(3, 41), random.Next(20, 50));
            List<Node> path2 = new PathGenerator(grid).getPath(new Node(path1Dest.row, path1Dest.col), path2Dest);

            foreach (Node node in path2)
            {
                grid[node.row, node.col] = 1;
            }

            List<Node> path3 = new PathGenerator(grid).getPath(new Node(path2Dest.row,path2Dest.col), new Node(41,76));

            foreach (Node node in path3)
            {
                grid[node.row, node.col] = 1;
            }

            path1.AddRange(path2);
            path1.AddRange(path3);
            return path1;
        }

    }
}
