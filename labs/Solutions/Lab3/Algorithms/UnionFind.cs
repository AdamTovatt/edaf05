namespace Lab3.Algorithms
{
    public class UnionFind
    {
        // contains the id's of parents for a certain other id, a bit confusing, but it's like the index is the key and the value at that index is the value
        private readonly int[] _parent; // parent[i] is the parent of node i in the union-find structure

        // contains the rank (depth of a tree) that has the root that is on a given index in this array, again like the index is the key and the value at that index is the value
        private readonly int[] _rank;   // so rank[i] is an estimation of depth that is relevant (only) if the node with id/index i is a root node

        public UnionFind(int size)
        {
            _parent = new int[size + 1]; // +1 to use 1-based indexing (people are numbered from 1)
            _rank = new int[size + 1];

            for (int i = 1; i <= size; i++)
                _parent[i] = i; // initially, each node is its own parent
        }

        public int FindRoot(int id) // finds the root of a person with the provided id
        {
            if (_parent[id] != id)
                _parent[id] = FindRoot(_parent[id]); // recursively call FindRoot again with the closest parent untill we hit the root

            return _parent[id];
        }

        public bool Union(int idA, int idB) // will set two sets as being connected
        {
            int rootA = FindRoot(idA); // find root of the person with idA
            int rootB = FindRoot(idB); // find root of the person with idB

            if (rootA == rootB)
                return false; // already connected

            if (_rank[rootA] < _rank[rootB])
            {
                _parent[rootA] = rootB; // attach smaller tree under larger tree
            }
            else if (_rank[rootA] > _rank[rootB])
            {
                _parent[rootB] = rootA;
            }
            else
            {
                _parent[rootB] = rootA;
                _rank[rootA]++; // increase rank only when ranks are equal

                // the reason we only increment the rank when the trees have equal ranks is because then 
                // one of the trees will unavoidably increase one in rank as it gets a new parent

                // if the trees are not of equal rank we add the smaller tree under the larger tree which means
                // that the rank doesn't change since it represents the max depth and the larger tree is
                // the one with max depth of the two before the union as well as after the union
                // meaning that the rank remains unchanged
            }

            return true; // union successful
        }
    }
}
