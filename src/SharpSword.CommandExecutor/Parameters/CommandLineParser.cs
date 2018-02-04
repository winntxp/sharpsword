/* *******************************************************
 * SharpSword zhangliang4629@163.com 11/22/2016 11:41:55 AM
 * ****************************************************************/
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace SharpSword.CommandExecutor.Parameters
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandLineParser : ICommandLineParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandLine"></param>
        /// <returns></returns>
        [SecurityCritical]
        public IEnumerable<string> Parse(string commandLine)
        {
            return SplitArgs(commandLine);
        }

        /// <summary>
        /// 
        /// </summary>
        public class State
        {
            private readonly string _commandLine;
            private readonly StringBuilder _stringBuilder;
            private readonly List<string> _arguments;
            private int _index;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="commandLine"></param>
            public State(string commandLine)
            {
                _commandLine = commandLine;
                _stringBuilder = new StringBuilder();
                _arguments = new List<string>();
            }

            /// <summary>
            /// 
            /// </summary>
            public StringBuilder StringBuilder { get { return _stringBuilder; } }

            /// <summary>
            /// 
            /// </summary>
            public bool EOF { get { return _index >= _commandLine.Length; } }

            /// <summary>
            /// 
            /// </summary>
            public char Current { get { return _commandLine[_index]; } }

            /// <summary>
            /// 
            /// </summary>
            public IEnumerable<string> Arguments { get { return _arguments; } }

            /// <summary>
            /// 
            /// </summary>
            public void AddArgument()
            {
                _arguments.Add(StringBuilder.ToString());
                StringBuilder.Clear();
            }

            /// <summary>
            /// 
            /// </summary>
            public void AppendCurrent()
            {
                StringBuilder.Append(Current);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="ch"></param>
            public void Append(char ch)
            {
                StringBuilder.Append(ch);
            }

            /// <summary>
            /// 
            /// </summary>
            public void MoveNext()
            {
                if (!EOF)
                    _index++;
            }
        }

        /// <summary>
        /// Implement the same logic as found at
        /// http://msdn.microsoft.com/en-us/library/17w5ykft.aspx
        /// The 3 special characters are quote, backslash and whitespaces, in order 
        /// of priority.
        /// The semantics of a quote is: whatever the state of the lexer, copy
        /// all characters verbatim until the next quote or EOF.
        /// The semantics of backslash is: If the next character is a backslash or a quote,
        /// copy the next character. Otherwise, copy the backslash and the next character.
        /// The semantics of whitespace is: end the current argument and move on to the next one.
        /// </summary>
        private IEnumerable<string> SplitArgs(string commandLine)
        {
            var state = new State(commandLine);
            while (!state.EOF)
            {
                switch (state.Current)
                {
                    case '"':
                        ProcessQuote(state);
                        break;

                    case '\\':
                        ProcessBackslash(state);
                        break;

                    case ' ':
                    case '\t':
                        if (state.StringBuilder.Length > 0)
                            state.AddArgument();
                        state.MoveNext();
                        break;

                    default:
                        state.AppendCurrent();
                        state.MoveNext();
                        break;
                }
            }
            if (state.StringBuilder.Length > 0)
                state.AddArgument();
            return state.Arguments;
        }

        private void ProcessQuote(State state)
        {
            state.MoveNext();
            while (!state.EOF)
            {
                if (state.Current == '"')
                {
                    state.MoveNext();
                    break;
                }
                state.AppendCurrent();
                state.MoveNext();
            }

            state.AddArgument();
        }

        private void ProcessBackslash(State state)
        {
            state.MoveNext();
            if (state.EOF)
            {
                state.Append('\\');
                return;
            }

            if (state.Current == '"')
            {
                state.Append('"');
                state.MoveNext();
            }
            else
            {
                state.Append('\\');
                state.AppendCurrent();
                state.MoveNext();
            }
        }
    }
}
