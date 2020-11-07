using test_project.attributes;
using System;
using System.Linq;

namespace test_project.tasks {
    [Task()]
    public class QuickSort : ITask {
        private int[] _input;

        public void Run(params string[] args) {
            _input = args.Select(int.Parse).ToArray();
            Console.WriteLine($"Unsorted: {string.Join(", ", _input)}");

            qs(0, _input.Length - 1);
            Console.WriteLine($"Sorted: {string.Join(", ", _input)}");
        }

        private void qs(int l, int r) {
            if(l >= r) return;

            var p = partition(l, r);

            qs(l, p - 1);
            qs(p + 1, r);
        }

        private int partition(int l, int r) {
            var pivot = _input[r];
            var i = l - 1;
            
            var temp = -1;
            for(var j = l; j < r; j++) {
                if(_input[j] < pivot) {
                    i += 1;
                    temp = _input[i];
                    _input[i] = _input[j];
                    _input[j] = temp;
                }
            }
            
            temp = _input[i + 1];
            _input[i + 1] = _input[r];
            _input[r] = temp;

            return i + 1;
        }
    }
}
