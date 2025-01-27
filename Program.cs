namespace DominoChainSolver {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Circular Domino Chain Solver");
            
            var dominos = new [] { (1, 2), (2, 3), (3, 1) };

            var solver = new DominoSolver(dominos);
            var result = solver.FindCircularChain();

            if (result is { Count: > 0 }) {
                Console.WriteLine("Found a circular chain:");
                foreach (var domino in result) {
                    Console.Write($"[{domino.Item1}|{domino.Item2}] ");
                }
            } else {
                Console.WriteLine("No circular chain found.");
            }
        }
    }
}