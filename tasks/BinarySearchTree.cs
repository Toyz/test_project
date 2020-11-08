using System;
using System.Linq;

using test_project.attributes;

namespace test_project.tasks
{
    [Task()]
    public class BinarySearchTree : ITask {
        private Node _root;
        private Tree _tree;
        private int _target;

        public void Init(params string[] args) {
            _target =  int.Parse(args[0]);
            var treeItems = args.Skip(1).Select(int.Parse).ToArray();

            _tree = new Tree();
            foreach(var item in treeItems) {
                _root = _tree.Insert(_root, item);
            }
        }

        public void Run() {
            var pre = _tree.Find(_root, _target, Direction.Predecessor);
            var suc = _tree.Find(_root, _target, Direction.Successor);

            Console.WriteLine($"Search Target: {_target}");
            if(pre == null) {
                Console.WriteLine($"Predecessor was not found");
            } else {
                Console.WriteLine($"Predecessor: {pre.Value}");
            }

            if(suc == null) {
                Console.WriteLine($"Successor was not found");
            } else {
                Console.WriteLine($"Successor: {suc.Value}");
            }
        }

        /* Tree stuff */
        public enum Direction {
            Predecessor,
            Successor
        }

        private class Node {
            public int Value { get; set; }
            public Node Left { get;set; }
            public Node Right { get;set; }
        }

        private class Tree {
            public Node Insert(Node root, int value) {
                if(root == null) {
                    root = new Node {
                        Value = value
                    };
                }
                else if (value < root.Value) {
                    root.Left = Insert(root.Left, value);
                } else {
                    root.Right = Insert(root.Right, value);
                }

                return root;
            }

            public void Traverse(Node root) {
                if(root == null) {
                    return;
                }

                Traverse(root.Left);
                Traverse(root.Right);
            }

            public Node Find(Node root, int value, Direction direction) {
                if(root == null) return null;

                Node node = null;
                if(root.Value == value) {
                    switch(direction) {
                        case Direction.Predecessor when root.Left != null:
                            node = FindPredecessor(root);
                            break;
                        case Direction.Successor when root.Right != null:
                            node = FindSuccessor(root);
                            break;
                    }

                    return node;
                }   

                if(root.Value > value) {
                    return Find(root.Left, value, direction);
                } else {
                    return Find(root.Right, value, direction);
                }
            }

            private Node FindPredecessor(Node node) {
                Node tmp = node.Left;
                if(tmp == null) return tmp;

                while(tmp != null && tmp.Right != null)
                    tmp = tmp.Right;
                return tmp;
            }

            private Node FindSuccessor(Node node) {
                Node tmp = node.Right;
                while(tmp != null && tmp.Left != null)
                    tmp = tmp.Left;
                return tmp;
            }
        }
    }
}