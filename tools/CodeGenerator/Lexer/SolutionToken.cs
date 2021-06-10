using System;
using System.Globalization;
using System.IO;

namespace CodeGenerator.Lexer
{
    public class SolutionToken : SolutionNode
    {
        public SolutionToken(NodeType type) : base(type)
        {
            SlotCount = 0;
        }

        public override SolutionNode GetSlot(int index)
        {
            throw new InvalidOperationException();
        }

        public virtual string Text
        {
            get
            {
                return TokenFactory.GetText(Type);
            }
        }

        public sealed override bool IsToken => true;

        public override string ToString()
        {
            return Text;
        }

        public virtual object Value
        {
            get
            {
                return Text;
            }
        }

        public override object GetValue()
        {
            return Value;
        }

        public virtual string ValueText
        {
            get { return Text; }
        }

        public override string GetValueText()
        {
            return ValueText;
        }

        protected override void WriteTokenTo(TextWriter writer)
        {
            writer.Write(Text);
        }


        public static SolutionToken Token(NodeType type)
        {
            return new SolutionToken(type);
        }


        public static SolutionToken WithValue<T>(NodeType type, T value)
        {
            return new SolutionTokenWithValue<T>(type, value.ToString() ,value);
        }

        public static SolutionToken Version(string text)
        {
            var version = System.Version.Parse(text);
            return new SolutionTokenWithValue<Version>(NodeType.VersionToken, text, version);
        }

        public static SolutionToken Number(int number)
        {
            return new SolutionTokenWithValue<int>(NodeType.StringToken, number.ToString(), number);
        }

        public static SolutionToken String(string text)
        {
            return new SolutionTokenWithValue<string>(NodeType.StringToken, text, text);
        }

        private class SolutionTokenWithValue<T> : SolutionToken
        {
            protected readonly string TextField;
            protected readonly T ValueField;

            public SolutionTokenWithValue(NodeType type, string text, T value) : base(type)
            {
                TextField = text;
                ValueField = value;
            }
            public override string Text => TextField;
            public override object Value => ValueField;
            public override string ValueText
            {
                get
                {
                    return Convert.ToString(ValueField, CultureInfo.InvariantCulture);
                }
            }


        }
    }
}
