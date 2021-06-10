using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace CodeGenerator.Lexer
{
    public class SolutionLexer : IDisposable
    {
        private readonly StringBuilder _builder = new StringBuilder();
        private readonly TextWindow TextWindow;

        public SolutionLexer(string text)
            : this(new TextWindow(text))
        {
        }

        public SolutionLexer(TextWindow textWindow)
        {
            TextWindow = textWindow;
        }

        public void Dispose()
        {
            TextWindow?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Start()
        {
            TextWindow.Start();
        }

        private struct TokenInfo
        {
            public NodeType Type;
            public string Text;
            public Version Version;
            public int Number;
        }

        public ImmutableArray<SolutionToken> LexSolutionTokens()
        {
            var builder = ImmutableArray.CreateBuilder<SolutionToken>();

            var current = LexSolutionToken();

            while(current.Type != NodeType.EndOfFileToken)
            {
                builder.Add(current);
                current = LexSolutionToken();
            }

            return builder.ToImmutable();
        }

        public SolutionToken LexSolutionToken()
        {
            if (TextWindow.PeekChar() == char.MaxValue)
            {
                return TokenFactory.EndOfFile;
            }

            var tokenInfo = default(TokenInfo);

            Start();

            ScanSolutionToken(ref tokenInfo);

            return Create(ref tokenInfo);
        }

        private void ScanSolutionToken(ref TokenInfo info)
        {
            info.Type = NodeType.None;
            info.Text = null;
            char character;
            int startingPosition = TextWindow.Position;
            char c = TextWindow.PeekChar();

            switch (c)
            {
                case 'M':
                    {
                        TextWindow.AdvanceChar(2);
                        character = TextWindow.PeekChar();
                        switch (character)
                        {
                            case 'c':
                                {
                                    info.Type = NodeType.MicrosoftKeyword;
                                    TextWindow.AdvanceChar(7);
                                    break;
                                }
                            case 'n':
                                {
                                    info.Type = NodeType.MinimumVisualStudioVersionKeyword;
                                    TextWindow.AdvanceChar(24);
                                    break;
                                }
                        }

                        break;
                    }
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    {
                        ScanNumber(ref info);
                        break;
                    }
                case '\"':
                    {
                        ScanString(ref info);
                        break;
                    }
                case ' ':
                    {
                        TextWindow.AdvanceChar();
                        info.Type = NodeType.SpaceToken;
                        break;
                    }
                case '\t':
                    {
                        TextWindow.AdvanceChar();
                        info.Type = NodeType.TabToken;
                        break;
                    }
                case '#':
                    {
                        TextWindow.AdvanceChar();
                        info.Type = NodeType.EqualSignToken;
                        break;
                    }
                case '=':
                    {
                        TextWindow.AdvanceChar();
                        info.Type = NodeType.EqualSignToken;
                        break;
                    }
                case '.':
                    {
                        TextWindow.AdvanceChar();
                        info.Type = NodeType.DotToken;
                        break;
                    }
                case ',':
                    {
                        TextWindow.AdvanceChar();
                        info.Type = NodeType.CommaToken;
                        break;
                    }
                case '{':
                    {
                        TextWindow.AdvanceChar();
                        info.Type = NodeType.OpenBraceToken;
                        break;
                    }
                case '}':
                    {
                        TextWindow.AdvanceChar();
                        info.Type = NodeType.CloseBraceToken;
                        break;
                    }
                case '(':
                    {
                        TextWindow.AdvanceChar();
                        info.Type = NodeType.OpenParenToken;
                        break;
                    }
                case ')':
                    {
                        TextWindow.AdvanceChar();
                        info.Type = NodeType.CloseParenToken;
                        break;
                    }
            }
        }

        private void ScanNumber(ref TokenInfo info)
        {
            _builder.Clear();

            TextWindow.AdvanceChar();
            info.Type = NodeType.NumberToken;

            while (true)
            {
                var ch = TextWindow.PeekChar();

                if (char.IsDigit(ch))
                {
                    TextWindow.AdvanceChar();
                    _builder.Append(ch);
                }
                else
                {
                    TextWindow.AdvanceChar();
                    break;
                }
            }

            info.Text = _builder.ToString();
            info.Number = int.Parse(info.Text);
        }

        private void ScanString(ref TokenInfo info)
        {
            _builder.Clear();
            var quoteCharacter = TextWindow.PeekChar();
            TextWindow.AdvanceChar();
            while (true)
            {
                var ch = TextWindow.PeekChar();
                if (ch != quoteCharacter)
                {
                    TextWindow.AdvanceChar();
                    break;
                }
                else
                {
                    TextWindow.AdvanceChar();
                    _builder.Append(ch);
                }
            }
            info.Text = TextWindow.Intern(_builder);
            info.Type = NodeType.StringToken;
        }


        private SolutionToken Create(ref TokenInfo info)
        {
            switch (info.Type)
            {
                case NodeType.MicrosoftKeyword: return TokenFactory.Microsoft;
                case NodeType.VisualKeyword: return TokenFactory.Visual;
                case NodeType.StudioKeyword: return TokenFactory.Studio;
                case NodeType.SolutionKeyword: return TokenFactory.Solution;
                case NodeType.FileKeyword: return TokenFactory.File;
                case NodeType.FormatKeyword: return TokenFactory.Format;
                case NodeType.VersionKeyword: return TokenFactory.Version;
                case NodeType.VisualStudioVersionKeyword: return TokenFactory.VisualStudioVersion;
                case NodeType.MinimumVisualStudioVersionKeyword: return TokenFactory.MinimumVisualStudioVersion;
                case NodeType.ProjectKeyword: return TokenFactory.Project;
                case NodeType.EndProjectKeyword: return TokenFactory.EndProject;
                case NodeType.GlobalKeyword: return TokenFactory.Global;
                case NodeType.EndGlobalKeyword: return TokenFactory.EndGlobal;
                case NodeType.GlobalSectionKeyword: return TokenFactory.GlobalSection;
                case NodeType.EndGlobalSectionKeyword: return TokenFactory.EndGlobalSection;
                case NodeType.PreSolutionKeyword: return TokenFactory.PreSolution;
                case NodeType.PostSolutionKeyword: return TokenFactory.PostSolution;
                case NodeType.SolutionConfigurationPlatformsKeyword: return TokenFactory.SolutionConfigurationPlatforms;
                case NodeType.ProjectConfigurationPlatformsKeyword: return TokenFactory.ProjectConfigurationPlatforms;
                case NodeType.SolutionPropertiesKeyword: return TokenFactory.SolutionProperties;
                case NodeType.NestedProjectsKeyword: return TokenFactory.NestedProjects;
                case NodeType.ExtensibilityGlobalsKeyword: return TokenFactory.ExtensibilityGlobals;
                case NodeType.SolutionGuidKeyword: return TokenFactory.SolutionGuid;
                case NodeType.DotToken: return TokenFactory.Dot;
                case NodeType.HashToken: return TokenFactory.Hash;
                case NodeType.CommaToken: return TokenFactory.Comma;
                case NodeType.EqualSignToken: return TokenFactory.EqualSign;
                case NodeType.OpenParenToken: return TokenFactory.OpenParen;
                case NodeType.CloseParenToken: return TokenFactory.CloseParen;
                case NodeType.OpenBraceToken: return TokenFactory.OpenBrace;
                case NodeType.CloseBraceToken: return TokenFactory.CloseBrace;
                case NodeType.TabToken: return TokenFactory.Tab;
                case NodeType.SpaceToken: return TokenFactory.Space;
                case NodeType.NewLineToken: return TokenFactory.NewLine;
                case NodeType.ByteOrderMarkToken: return TokenFactory.ByteOrderMark;


                case NodeType.StringToken: return TokenFactory.String(info.Text);
                case NodeType.NumberToken: return TokenFactory.Number(int.Parse(info.Text));

                case NodeType.EndOfFileToken: return TokenFactory.EndOfFile;

                default: return null;
            }
        }

    }
}
