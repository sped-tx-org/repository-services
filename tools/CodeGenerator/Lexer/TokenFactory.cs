using System;
using System.Text;

namespace CodeGenerator.Lexer
{
    internal static class TokenFactory
    {

        private static SolutionToken Token(NodeType type) => SolutionToken.Token(type);
        public static SolutionToken EndOfFile => Token(NodeType.EndOfFileToken);
        public static SolutionToken Microsoft => Token(NodeType.MicrosoftKeyword);
        public static SolutionToken Visual => Token(NodeType.VisualKeyword);
        public static SolutionToken Studio => Token(NodeType.StudioKeyword);
        public static SolutionToken Solution => Token(NodeType.SolutionKeyword);
        public static SolutionToken File => Token(NodeType.FileKeyword);
        public static SolutionToken Format => Token(NodeType.FormatKeyword);
        public static SolutionToken Version => Token(NodeType.VersionKeyword);
        public static SolutionToken VisualStudioVersion => Token(NodeType.VisualStudioVersionKeyword);
        public static SolutionToken MinimumVisualStudioVersion => Token(NodeType.MinimumVisualStudioVersionKeyword);
        public static SolutionToken Project => Token(NodeType.ProjectKeyword);
        public static SolutionToken EndProject => Token(NodeType.EndProjectKeyword);
        public static SolutionToken Global => Token(NodeType.GlobalKeyword);
        public static SolutionToken EndGlobal => Token(NodeType.EndGlobalKeyword);
        public static SolutionToken GlobalSection => Token(NodeType.GlobalSectionKeyword);
        public static SolutionToken EndGlobalSection => Token(NodeType.EndGlobalSectionKeyword);
        public static SolutionToken PreSolution => Token(NodeType.PreSolutionKeyword);
        public static SolutionToken PostSolution => Token(NodeType.PostSolutionKeyword);
        public static SolutionToken SolutionConfigurationPlatforms => Token(NodeType.SolutionConfigurationPlatformsKeyword);
        public static SolutionToken ProjectConfigurationPlatforms => Token(NodeType.ProjectConfigurationPlatformsKeyword);
        public static SolutionToken SolutionProperties => Token(NodeType.SolutionPropertiesKeyword);
        public static SolutionToken NestedProjects => Token(NodeType.NestedProjectsKeyword);
        public static SolutionToken ExtensibilityGlobals => Token(NodeType.ExtensibilityGlobalsKeyword);
        public static SolutionToken SolutionGuid => Token(NodeType.SolutionGuidKeyword);
        public static SolutionToken String(string text) => SolutionToken.String(text);
        public static SolutionToken Number(int number) => SolutionToken.Number(number);
        public static SolutionToken VersionToken => Token(NodeType.VersionToken);
        public static SolutionToken Key => Token(NodeType.KeyToken);
        public static SolutionToken Value => Token(NodeType.ValueToken);
        public static SolutionToken Dot => Token(NodeType.DotToken);
        public static SolutionToken Hash => Token(NodeType.HashToken);
        public static SolutionToken Comma => Token(NodeType.CommaToken);
        public static SolutionToken EqualSign => Token(NodeType.EqualSignToken);
        public static SolutionToken OpenParen => Token(NodeType.OpenParenToken);
        public static SolutionToken CloseParen => Token(NodeType.CloseParenToken);
        public static SolutionToken OpenBrace => Token(NodeType.OpenBraceToken);
        public static SolutionToken CloseBrace => Token(NodeType.CloseBraceToken);
        public static SolutionToken Tab => Token(NodeType.TabToken);
        public static SolutionToken Space => Token(NodeType.SpaceToken);
        public static SolutionToken NewLine => Token(NodeType.NewLineToken);
        public static SolutionToken ByteOrderMark => Token(NodeType.ByteOrderMarkToken);


        public static string GetText(NodeType nodeType)
        {
            switch (nodeType)
            {
                case NodeType.MicrosoftKeyword: return "Microsoft";
                case NodeType.VisualKeyword: return "Visual";
                case NodeType.StudioKeyword: return "Studio";
                case NodeType.SolutionKeyword: return "Solution";
                case NodeType.FileKeyword: return "File";
                case NodeType.FormatKeyword: return "Format";
                case NodeType.VersionKeyword: return "VisualStudioVersion";
                case NodeType.VisualStudioVersionKeyword: return "";
                case NodeType.MinimumVisualStudioVersionKeyword: return "MinimumVisualStudioVersion";
                case NodeType.ProjectKeyword: return "Project";
                case NodeType.EndProjectKeyword: return "EndProject";
                case NodeType.GlobalKeyword: return "Global";
                case NodeType.EndGlobalKeyword: return "EndGlobal";
                case NodeType.GlobalSectionKeyword: return "GlobalSection";
                case NodeType.EndGlobalSectionKeyword: return "EndGlobalSection";
                case NodeType.PreSolutionKeyword: return "preSolution";
                case NodeType.PostSolutionKeyword: return "postSolution";
                case NodeType.SolutionConfigurationPlatformsKeyword: return "SolutionConfigurationPlatforms";
                case NodeType.ProjectConfigurationPlatformsKeyword: return "ProjectConfigurationPlatforms";
                case NodeType.SolutionPropertiesKeyword: return "SolutionProperties";
                case NodeType.NestedProjectsKeyword: return "NestedProjects";
                case NodeType.ExtensibilityGlobalsKeyword: return "ExtensibilityGlobals";
                case NodeType.SolutionGuidKeyword: return "SolutionGuid";
                case NodeType.DotToken: return ".";
                case NodeType.HashToken: return "#";
                case NodeType.CommaToken: return ",";
                case NodeType.EqualSignToken: return "=";
                case NodeType.OpenParenToken: return "(";
                case NodeType.CloseParenToken: return ")";
                case NodeType.OpenBraceToken: return "{";
                case NodeType.CloseBraceToken: return "}";
                case NodeType.SpaceToken: return " ";
                case NodeType.TabToken: return "\t";
                case NodeType.ByteOrderMarkToken: return Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
                case NodeType.NewLineToken: return Environment.NewLine;
                default: return null;
            }
        }

        public static bool IsToken(NodeType nodeType)
        {
            switch (nodeType)
            {
                case NodeType.StringToken:
                case NodeType.NumberToken:
                case NodeType.VersionToken:
                case NodeType.CommaToken:
                case NodeType.EqualSignToken:
                case NodeType.OpenParenToken:
                case NodeType.CloseParenToken:
                case NodeType.OpenBraceToken:
                case NodeType.CloseBraceToken:
                case NodeType.KeyToken:
                case NodeType.ValueToken:
                case NodeType.SpaceToken:
                case NodeType.TabToken:
                case NodeType.NewLineToken:
                case NodeType.ByteOrderMarkToken:
                case NodeType.EndOfFileToken:
                    return true;
                default: return false;
            }
        }

        public static bool IsKeyword(NodeType token)
        {
            switch (token)
            {
                case NodeType.MicrosoftKeyword:
                case NodeType.VisualKeyword:
                case NodeType.StudioKeyword:
                case NodeType.SolutionKeyword:
                case NodeType.FileKeyword:
                case NodeType.FormatKeyword:
                case NodeType.VersionKeyword:
                case NodeType.VisualStudioVersionKeyword:
                case NodeType.MinimumVisualStudioVersionKeyword:
                case NodeType.ProjectKeyword:
                case NodeType.EndProjectKeyword:
                case NodeType.GlobalKeyword:
                case NodeType.EndGlobalKeyword:
                case NodeType.GlobalSectionKeyword:
                case NodeType.EndGlobalSectionKeyword:
                case NodeType.PreSolutionKeyword:
                case NodeType.PostSolutionKeyword:
                    return true;
                default:
                    return false;
            }
        }
    }
}

