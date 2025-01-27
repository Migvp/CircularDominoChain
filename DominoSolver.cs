namespace DominoChainSolver;

internal sealed class DominoSolver {
    private readonly Dictionary<int, List<int>> _adjacencyList = new();
    private readonly List<(int, int)> _dominos;

    public DominoSolver((int, int)[] inputDominos) {
        _dominos = new List<(int, int)>(inputDominos);

        foreach (var (a, b) in inputDominos) {
            if (!_adjacencyList.ContainsKey(a)) {
                _adjacencyList[a] = [];
            }
            if (!_adjacencyList.ContainsKey(b)) {
                _adjacencyList[b] = [];
            }
            
            _adjacencyList[a].Add(b);
            _adjacencyList[b].Add(a);
        }
    }

    private bool CanFormCircularChain() {
        foreach (var vertex in _adjacencyList.Keys) {
            if (_adjacencyList[vertex].Count % 2 != 0)
                return false;
        }
        return true;
    }

    public List<(int, int)>? FindCircularChain() {
        if (!CanFormCircularChain()) {
            return null;
        }
        
        var circuit = ConstructDominoPath();

        return ReconstructDominoChain(circuit);
    }
    
    private List<int> ConstructDominoPath() {
        var stack = new Stack<int>();
        var circuit = new List<int>();

        // Start from any vertex
        stack.Push(_dominos[0].Item1);
        
        while (stack.Count > 0) {
            var current = stack.Peek();
            if (_adjacencyList[current].Count > 0) {
                var next = _adjacencyList[current][0];
                _adjacencyList[current].Remove(next);
                _adjacencyList[next].Remove(current);
                stack.Push(next);
            } else {
                circuit.Add(stack.Pop());
            }
        }

        return circuit;
    }

    private List<(int, int)> ReconstructDominoChain(List<int> circuit) {
        var chain = new List<(int, int)>();
        for (var i = 1; i < circuit.Count; i++) {
            var a = circuit[i - 1];
            var b = circuit[i];
            var domino = (a, b);

            chain.Add(domino);
        }

        return chain;
    } 
}