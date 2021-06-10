using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Immutable;
using System.Threading.Tasks;
using System.IO;

namespace CodeGenerator.Lexer
{

    public abstract class SolutionNode
    {
        protected SolutionNode(NodeType type)
        {
            Type = type;
        }

        protected int SlotCount;

        public abstract SolutionNode GetSlot(int index);

        public NodeType Type { get; }

        private static int GetFirstNonNullChildIndex(SolutionNode node)
        {
            int n = node.SlotCount;
            int firstIndex = 0;
            for (; firstIndex < n; firstIndex++)
            {
                var child = node.GetSlot(firstIndex);
                if (child != null)
                {
                    break;
                }
            }

            return firstIndex;
        }

        private static int GetLastNonNullChildIndex(SolutionNode node)
        {
            int n = node.SlotCount;
            int lastIndex = n - 1;
            for (; lastIndex >= 0; lastIndex--)
            {
                var child = node.GetSlot(lastIndex);
                if (child != null)
                {
                    break;
                }
            }

            return lastIndex;
        }

        public virtual bool IsToken => false;
        public virtual object GetValue() { return null; }
        public virtual string GetValueText() { return string.Empty; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var writer = new StringWriter(sb, System.Globalization.CultureInfo.InvariantCulture);
            WriteTo(writer);
            return sb.ToString();
        }

        protected virtual void WriteTokenTo(TextWriter writer)
        {

        }

        public virtual void WriteTo(TextWriter writer)
        {
            var stack = new Stack<SolutionNode>();
            stack.Push(this);
            ProcessStack(writer, stack);
        }

        private static void ProcessStack(TextWriter writer, Stack<SolutionNode> stack)
        {
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current.IsToken)
                {
                    current.WriteTokenTo(writer);
                    continue;
                }
                var firstIndex = GetFirstNonNullChildIndex(current);
                var lastIndex = GetLastNonNullChildIndex(current);
                for (var i = lastIndex; i >= firstIndex; i--)
                {
                    var child = current.GetSlot(i);
                    if (child != null)
                    {
                        stack.Push(child);
                    }
                }
            }
        }
    }
}
