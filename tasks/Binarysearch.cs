using System;
using System.Linq;
using test_project.attributes; 

namespace test_project.tasks
{
    // This uses QuickSort based on Quicksort.cs to sort the input data;
    [Task()]
    public class Binarysearch : ITask {
        private int[] _input;
        private int _target;

        public void Init(params string[] args) {
            _target =  int.Parse(args[0]);
            _input = args.Skip(1).Select(int.Parse).ToArray();
        }

        public void Run() {
            qs(0, _input.Length - 1);
            
            var res = search();
            if(res == null) {
                Console.Error.WriteLine($"Failed to find {_target} in {string.Join(", ", _input)}");
                return;
            }

            Console.WriteLine($"Found target {_target} as index {res} in {string.Join(", ", _input)}");
        }

        // Nullable so if we didn't find a result we can return null
        private int? search() {
            int left = 0;
            int right = _input.Length - 1; 
            while(left <= right) {
                var mid = (left + right) / 2;
                if(_input[mid] == _target) return mid;
                else if(_target < _input[mid]) right = mid - 1;
                else left = mid + 1;
            }

            return null;
        }

        // Quicksort from Quicsort.cs
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