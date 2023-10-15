using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower.Levels
{
    public class Node
    {
        public int row;
        public int col;
        public Node parent;
        public bool visited = false;

        public List<Node> neighbors;
        public float g, h;

        public Node(int row, int col, Node parent = null)
        {
            this.row = row;
            this.col = col;
            this.parent = parent;
            this.visited = false;
            neighbors = new List<Node>();

            g = 0;
            h = 0;
        }

        public bool isEqual(Node other)
        {
            return row == other.row && col == other.col;
        }
        public float f()
        {
            return g + h;
        }

         public float euclidean_distance(Node node1, Node node2)
        {
            return (float)Math.Sqrt((node2.row - node1.row) * (node2.row - node1.row) + (node2.col - node2.col) * (node2.col - node2.col));
        }
    }
}
