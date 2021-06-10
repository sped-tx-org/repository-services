using System;
using System.Text;
using Microsoft.VisualBasic;

namespace CodeGenerator.Lexer
{
    public class TextWindow : IDisposable
    {
        public const char InvalidCharacter = char.MaxValue;
        private const int DefaultWindowLength = 2048;

        private readonly string _text;                     // Source of text to parse.
        private int _basis;                                // Offset of the window relative to the SourceText start.
        private int _offset;                               // Offset from the start of the window.
        private readonly int _textEnd;                     // Absolute end position
        private static char[] _characterWindow;                   // Moveable window of chars from source text
        private int _characterWindowCount;                 // # of valid characters in chars buffer

        private int _lexemeStart;                          // Start of current lexeme relative to the window start.

        private readonly StringTable _strings;
        public TextWindow(string text)
        {
            _text = text;
            _basis = 0;
            _offset = 0;
            _textEnd = text.Length;
            _strings = StringTable.GetInstance();
            _characterWindow = text.ToCharArray();
            _lexemeStart = 0;
        }

        public char[] CharacterWindow
        {
            get { return _characterWindow; }
        }
        public int CharacterWindowCount
        {
            get { return _characterWindowCount; }
        }
        public int LexemeRelativeStart
        {
            get { return _lexemeStart; }
        }
        public int LexemeStartPosition
        {
            get
            {
                return _basis + _lexemeStart;
            }
        }

        public int Offset
        {
            get { return _offset; }
        }
        public int Position
        {
            get
            {
                return _basis + _offset;
            }
        }
        public string Text
        {
            get { return _text; }
        }
        public int Width
        {
            get
            {
                return _offset - _lexemeStart;
            }
        }
        public void Start()
        {
            _lexemeStart = _offset;
        }

        public void AdvanceChar()
        {
            _offset++;
        }

        public void AdvanceChar(int n)
        {
            _offset += n;
        }

        public bool AdvanceIfMatches(string desired)
        {
            int length = desired.Length;

            for (int i = 0; i < length; i++)
            {
                if (PeekChar(i) != desired[i])
                {
                    return false;
                }
            }

            AdvanceChar(length);
            return true;
        }

        public void Dispose()
        {
            if (_characterWindow != null)
                _characterWindow = null;

            GC.SuppressFinalize(this);
        }

        public string GetInternedText()
        {
            return Intern(_characterWindow, _lexemeStart, Width);
        }

        public string GetText(bool intern)
        {
            return GetText(LexemeStartPosition, Width, intern);
        }

        public string GetText(int position, int length, bool intern)
        {
            int offset = position - _basis;

            // PERF: Whether interning or not, there are some frequently occurring
            // easy cases we can pick off easily.
            switch (length)
            {
                case 0:
                    return string.Empty;

                case 1:
                    if (_characterWindow[offset] == ' ')
                    {
                        return " ";
                    }
                    if (_characterWindow[offset] == '\n')
                    {
                        return "\n";
                    }
                    break;

                case 2:
                    char firstChar = _characterWindow[offset];
                    if (firstChar == '\r' && _characterWindow[offset + 1] == '\n')
                    {
                        return "\r\n";
                    }
                    if (firstChar == '/' && _characterWindow[offset + 1] == '/')
                    {
                        return "//";
                    }
                    break;

                case 3:
                    if (_characterWindow[offset] == '/' && _characterWindow[offset + 1] == '/' && _characterWindow[offset + 2] == ' ')
                    {
                        return "// ";
                    }
                    break;
            }

            if (intern)
            {
                return Intern(_characterWindow, offset, length);
            }
            else
            {
                return new string(_characterWindow, offset, length);
            }
        }

        public string Intern(char[] array, int start, int length)
        {
            return _strings.Add(array, start, length);
        }

        public string Intern(StringBuilder text)
        {
            return _strings.Add(text);
        }

        public bool IsReallyAtEnd()
        {
            return _offset >= _characterWindowCount && Position >= _textEnd;
        }

        public bool MoreChars()
        {
            if (_offset >= _characterWindowCount)
            {
                if (Position >= _textEnd)
                {
                    return false;
                }

                // if lexeme scanning is sufficiently into the char buffer, 
                // then refocus the window onto the lexeme
                if (_lexemeStart > (_characterWindowCount / 4))
                {
                    Array.Copy(_characterWindow,
                        _lexemeStart,
                        _characterWindow,
                        0,
                        _characterWindowCount - _lexemeStart);
                    _characterWindowCount -= _lexemeStart;
                    _offset -= _lexemeStart;
                    _basis += _lexemeStart;
                    _lexemeStart = 0;
                }

                if (_characterWindowCount >= _characterWindow.Length)
                {
                    // grow char array, since we need more contiguous space
                    char[] oldWindow = _characterWindow;
                    char[] newWindow = new char[_characterWindow.Length * 2];
                    Array.Copy(oldWindow, 0, newWindow, 0, _characterWindowCount);
                    _characterWindow = newWindow;
                }

                int amountToRead = Math.Min(_textEnd - (_basis + _characterWindowCount),
                    _characterWindow.Length - _characterWindowCount);
                _text.CopyTo(_basis + _characterWindowCount,
                    _characterWindow,
                    _characterWindowCount,
                    amountToRead);
                _characterWindowCount += amountToRead;
                return amountToRead > 0;
            }

            return true;
        }

        public char NextChar()
        {
            char c = PeekChar();
            if (c != InvalidCharacter)
            {
                AdvanceChar();
            }
            return c;
        }

        public char PeekChar()
        {
            if (_offset >= _characterWindowCount
                && !MoreChars())
            {
                return InvalidCharacter;
            }

            // N.B. MoreChars may update the offset.
            return _characterWindow[_offset];
        }

        public char PeekChar(int delta)
        {
            int position = Position;
            AdvanceChar(delta);

            char ch;
            if (_offset >= _characterWindowCount
                && !MoreChars())
            {
                ch = InvalidCharacter;
            }
            else
            {
                // N.B. MoreChars may update the offset.
                ch = _characterWindow[_offset];
            }

            Reset(position);
            return ch;
        }

        public void Reset(int position)
        {
            // if position is within already read character range then just use what we have
            int relative = position - _basis;
            if (relative >= 0 && relative <= _characterWindowCount)
            {
                _offset = relative;
            }
            else
            {
                // we need to reread text buffer
                int amountToRead = Math.Min(_text.Length, position + _characterWindow.Length) - position;
                amountToRead = Math.Max(amountToRead, 0);
                if (amountToRead > 0)
                {
                    _text.CopyTo(position, _characterWindow, 0, amountToRead);
                }

                _lexemeStart = 0;
                _offset = 0;
                _basis = position;
                _characterWindowCount = amountToRead;
            }
        }
    }
}
