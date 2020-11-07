using System;

namespace test_project.attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class TaskAttribute : Attribute {
        public string Name { get; private set; }

        public TaskAttribute(string Name = "") {
            this.Name = Name;
        }
    }
}