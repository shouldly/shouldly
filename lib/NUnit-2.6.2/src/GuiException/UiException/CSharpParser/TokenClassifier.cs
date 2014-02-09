// ****************************************************************
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************

using System;
using System.Collections.Generic;
using System.Text;

namespace NUnit.UiException.CodeFormatters
{
    /// <summary>
    /// Used at an internal stage to convert LexToken into ClassifiedToken. This class provides
    /// a very basic semantic analysis to make text following in one the categories below:
    ///     - regular code,
    ///     - developper comments,
    ///     - strings / character.
    /// The output of this class is used by CSharpCodeFormatter to achieve the basic syntax coloring.
    /// </summary>
    public class TokenClassifier
    {
        #region SMSTATE code

        // the list below contains constant values defining states for the finite
        // smState machine that makes all the work of converting LexToken into ClassifiedToken.
        // for instance, Lexer can send inputs like:
        //
        //   [Text][Separator][CommentC_Open][Text][CommentC_Close]
        //
        // This LexToken sequence can for instance be converted that way by TokenClassifier.
        //
        //   - [Text][Separator]                     => [Code]
        //   - [CommentC_Open][Text][CommentC_Close] => [Comment]
        // 

        /// <summary>
        /// State code for the smState machine.
        /// State when reaching a code block.
        /// </summary>
        public const int SMSTATE_CODE = 0;

        /// <summary>
        /// State code for the smState machine.
        /// State when reaching a C comment block.
        /// </summary>
        public const int SMSTATE_CCOMMENT = 1;

        /// <summary>
        /// State code for the smState machine.
        /// State when reaching a C++ comment block.
        /// </summary>
        public const int SMSTATE_CPPCOMMENT = 2;

        /// <summary>
        /// State code for the smState machine.
        /// State when reaching a char surrounded by single quotes.
        /// </summary>
        public const int SMSTATE_CHAR = 3;

        /// <summary>
        /// State code for the smState machine.
        /// State when reaching a string surrounded by double quotes.
        /// </summary>
        public const int SMSTATE_STRING = 4;

        #endregion

        /// <summary>
        /// A finite smState machine where states are: SMSTATE values and
        /// transitions are LexToken.
        /// </summary>
        private StateMachine _sm;

        /// <summary>
        /// The current StateMachine's SMTATE code.
        /// </summary>
        private int _sm_output;

        /// <summary>
        /// Makes a link between SMSTATE code and ClassificationTag.
        /// </summary>
        private Dictionary<int, ClassificationTag> _tags;

        /// <summary>
        /// Contains the list of C# keywords.
        /// </summary>
        private Dictionary<string, bool> _keywords;

        /// <summary>
        /// Indicate whether Lexer is in escaping mode.
        /// This flag is set to true when parsing "\\" and
        /// can influate on the following LexerTag value.
        /// </summary>
        private bool _escaping;

        /// <summary>
        /// Build a new instance of TokenClassifier.
        /// </summary>
        public TokenClassifier()
        {
            string[] words;

            _sm = new StateMachine();

            _tags = new Dictionary<int, ClassificationTag>();
            _tags.Add(SMSTATE_CODE, ClassificationTag.Code);
            _tags.Add(SMSTATE_CCOMMENT, ClassificationTag.Comment);
            _tags.Add(SMSTATE_CPPCOMMENT, ClassificationTag.Comment);
            _tags.Add(SMSTATE_CHAR, ClassificationTag.String);
            _tags.Add(SMSTATE_STRING, ClassificationTag.String);

            // build the list of predefined keywords.
            // this is from the official msdn site. Curiously, some keywords
            // were ommited from the official documentation.
            //   For instance "get", "set", "region" and "endregion" were
            // not part of the official list. Maybe it's a mistake or a misunderstanding
            // whatever... I want them paint in blue as well!

            words = new string[] {
                "abstract", "event", "new", "struct", "as", "explicit", "null", "switch",
                "base", "extern", "object", "this", "bool", "false", "operator", "throw",
                "break", "finally", "out", "true", "byte", "fixed", "override", "try", "case",
                "float", "params", "typeof", "catch", "for", "private", "uint", "char",
                "foreach", "protected", "ulong", "checked", "goto", "public", "unchecked",
                "class", "if", "readonly", "unsafe", "const", "implicit", "ref", "ushort",
                "continue", "in", "return", "using", "decimal", "int", "sbyte", "virtual",
                "default", "interface", "sealed", "volatile", "delegate", "internal",
                "short", "void", "do", "is", "sizeof", "while", "double", "lock", "stackalloc",
                "else", "long", "static", "enum", "namespace", "string", "partial", "get", "set",
                "region", "endregion",
            };

            _keywords = new Dictionary<string, bool>();
            foreach (string key in words)
                _keywords.Add(key, true);

            Reset();

            return;
        }

        /// <summary>
        /// Tells whether TokenClassifier is currently in escaping mode. When true,
        /// this flag causes TokenClassifier to override the final classification
        /// of a basic entity (such as: ") to be treated as normal text instead of
        /// being interpreted as a string delimiter.
        /// </summary>
        public bool Escaping
        {
            get { return (_escaping); }
        }

        /// <summary>
        /// Reset the StateMachine to default value. (code block).
        /// </summary>
        public void Reset()
        {
            _sm_output = SMSTATE_CODE;
            _escaping = false;

            return;
        }

        /// <summary>
        /// Classify the given LexToken into a ClassificationTag.
        /// </summary>
        /// <param name="token">The token to be classified.</param>
        /// <returns>The smState value.</returns>
        public ClassificationTag Classify(LexToken token)
        {
            int classTag;

            UiExceptionHelper.CheckNotNull(token, "token");

            classTag = AcceptLexToken(token);

            if (classTag == SMSTATE_CODE &&
                _keywords.ContainsKey(token.Text))
                return (ClassificationTag.Keyword);

            // Parsing a token whoose Text value is set to '\'
            // causes the classifier to set/reset is escaping mode.

            if (token.Text == "\\" &&
                _sm_output == SMSTATE_STRING &&
                !_escaping)
                _escaping = true;
            else
                _escaping = false;

            return (_tags[classTag]);
        }

        /// <summary>
        /// Classify the given token and get its corresponding SMSTATE value.
        /// </summary>
        /// <param name="token">The LexToken to be classified.</param>
        /// <returns>An SMSTATE value.</returns>
        protected int AcceptLexToken(LexToken token)
        {
            int smState;

            if (_escaping)
                return (SMSTATE_STRING);

            smState = GetTokenSMSTATE(_sm_output, token.Tag);
            _sm_output = GetSMSTATE(_sm_output, token.Tag);

            return (smState);
        }

        /// <summary>
        /// Gets the SMSTATE under the "transition" going from "smState".
        /// </summary>
        /// <param name="smState">The current smState.</param>
        /// <param name="transition">The current LexerTag.</param>
        /// <returns>The new smState.</returns>
        protected int GetSMSTATE(int smState, LexerTag transition)
        {
            return (_sm.GetSMSTATE(smState, transition));
        }

        /// <summary>
        /// Gets a token SMSTATE under the "transition" going from "smState".
        /// </summary>
        /// <param name="smState">The current smState machine.</param>
        /// <param name="transition">The LexerTag to be classified.</param>
        /// <returns>The LexerTag's classification.</returns>
        protected int GetTokenSMSTATE(int smState, LexerTag transition)
        {
            return (_sm.GetTokenSMSTATE(smState, transition));
        }

        #region StateMachine

        /// <summary>
        /// Defines a transition (of a state machine).
        /// </summary>
        class TransitionData
        {
            /// <summary>
            /// The current transition.
            /// </summary>
            public LexerTag Transition;

            /// <summary>
            /// The SMSTATE code reached when following that transition.
            /// </summary>
            public int SMSTATE;

            /// <summary>
            /// The TokenSMSTATE reached when following that transition.
            /// </summary>
            public int TokenSMSTATE;

            public TransitionData(LexerTag transition, int smState)
            {
                Transition = transition;

                SMSTATE = smState;
                TokenSMSTATE = smState;

                return;
            }

            public TransitionData(LexerTag transition, int smState, int tokenSmState) :
                this(transition, smState)
            {
                TokenSMSTATE = tokenSmState;
            }
        }

        /// <summary>
        /// Defines a state (of a state machine) and its associated transitions.
        /// </summary>
        class State
        {
            public int InitialState;
            public TransitionData[] Transitions;

            public State(int initialState, TransitionData[] transitions)
            {
                int i;
                int j;

                UiExceptionHelper.CheckNotNull(transitions, "transitions");
                UiExceptionHelper.CheckTrue(
                    transitions.Length == 8,
                    "expecting transitions.Length to be 8",
                    "transitions");

                for (i = 0; i < transitions.Length; ++i)
                    for (j = 0; j < transitions.Length; ++j)
                    {
                        if (j == i)
                            continue;

                        if (transitions[j].Transition == transitions[i].Transition)
                            UiExceptionHelper.CheckTrue(false,
                                String.Format("transition '{0}' already present", transitions[j].Transition),
                                "transitions");
                    }


                InitialState = initialState;
                Transitions = transitions;

                return;
            }

            public TransitionData this[LexerTag transition]
            {
                get
                {
                    foreach (TransitionData couple in Transitions)
                        if (couple.Transition == transition)
                            return (couple);
                    return (null);
                }
            }
        }

        /// <summary>
        /// A finite state machine. Where states are SMSTATE codes and
        /// transitions are LexTokens.
        /// </summary>
        class StateMachine
        {
            private State[] _states;

            public StateMachine()
            {
                _states = new State[5];

                // defines transitions from SMSTATE_CODE
                _states[0] = new State(
                    SMSTATE_CODE,
                    new TransitionData[] {
                        new TransitionData(LexerTag.EndOfLine, SMSTATE_CODE),
                        new TransitionData(LexerTag.Separator, SMSTATE_CODE),
                        new TransitionData(LexerTag.Text, SMSTATE_CODE),
                        new TransitionData(LexerTag.CommentC_Open, SMSTATE_CCOMMENT),
                        new TransitionData(LexerTag.CommentC_Close, SMSTATE_CODE, SMSTATE_CCOMMENT),
                        new TransitionData(LexerTag.CommentCpp, SMSTATE_CPPCOMMENT),
                        new TransitionData(LexerTag.SingleQuote, SMSTATE_CHAR),
                        new TransitionData(LexerTag.DoubleQuote, SMSTATE_STRING),
                    });

                // defines transitions from SMSTATE_CCOMMENT
                _states[1] = new State(
                    SMSTATE_CCOMMENT,
                    new TransitionData[] {
                        new TransitionData(LexerTag.EndOfLine, SMSTATE_CCOMMENT),
                        new TransitionData(LexerTag.Separator, SMSTATE_CCOMMENT),
                        new TransitionData(LexerTag.Text, SMSTATE_CCOMMENT),
                        new TransitionData(LexerTag.CommentC_Open, SMSTATE_CCOMMENT),
                        new TransitionData(LexerTag.CommentC_Close, SMSTATE_CODE, SMSTATE_CCOMMENT),
                        new TransitionData(LexerTag.CommentCpp, SMSTATE_CCOMMENT),
                        new TransitionData(LexerTag.SingleQuote, SMSTATE_CCOMMENT),
                        new TransitionData(LexerTag.DoubleQuote, SMSTATE_CCOMMENT),
                    });

                // defines transitions from SMSTATE_CPPCOMMENT
                _states[2] = new State(
                    SMSTATE_CPPCOMMENT,
                    new TransitionData[] {
                        new TransitionData(LexerTag.EndOfLine, SMSTATE_CODE),
                        new TransitionData(LexerTag.Separator, SMSTATE_CPPCOMMENT),
                        new TransitionData(LexerTag.Text, SMSTATE_CPPCOMMENT),
                        new TransitionData(LexerTag.CommentC_Open, SMSTATE_CPPCOMMENT),
                        new TransitionData(LexerTag.CommentC_Close, SMSTATE_CPPCOMMENT),
                        new TransitionData(LexerTag.CommentCpp, SMSTATE_CPPCOMMENT),
                        new TransitionData(LexerTag.SingleQuote, SMSTATE_CPPCOMMENT),
                        new TransitionData(LexerTag.DoubleQuote, SMSTATE_CPPCOMMENT),
                    });

                // defines transition from SMSTATE_CHAR
                _states[3] = new State(
                    SMSTATE_CHAR,
                    new TransitionData[] {
                        new TransitionData(LexerTag.EndOfLine, SMSTATE_CHAR),
                        new TransitionData(LexerTag.Separator, SMSTATE_CHAR),
                        new TransitionData(LexerTag.Text, SMSTATE_CHAR),
                        new TransitionData(LexerTag.CommentC_Open, SMSTATE_CHAR),
                        new TransitionData(LexerTag.CommentC_Close, SMSTATE_CHAR),
                        new TransitionData(LexerTag.CommentCpp, SMSTATE_CHAR),
                        new TransitionData(LexerTag.SingleQuote, SMSTATE_CODE, SMSTATE_CHAR),
                        new TransitionData(LexerTag.DoubleQuote, SMSTATE_CHAR),
                    });

                // defines transition from SMSTATE_STRING
                _states[4] = new State(
                    SMSTATE_STRING,
                    new TransitionData[] {
                        new TransitionData(LexerTag.EndOfLine, SMSTATE_STRING),
                        new TransitionData(LexerTag.Separator, SMSTATE_STRING),
                        new TransitionData(LexerTag.Text, SMSTATE_STRING),
                        new TransitionData(LexerTag.CommentC_Open, SMSTATE_STRING),
                        new TransitionData(LexerTag.CommentC_Close, SMSTATE_STRING),
                        new TransitionData(LexerTag.CommentCpp, SMSTATE_STRING),
                        new TransitionData(LexerTag.SingleQuote, SMSTATE_STRING),
                        new TransitionData(LexerTag.DoubleQuote, SMSTATE_CODE, SMSTATE_STRING),
                    });

                return;
            }

            /// <summary>
            /// Follow "transition" going from "smState" and returns reached SMSTATE.
            /// </summary>
            public int GetSMSTATE(int smState, LexerTag transition)
            {
                foreach (State st in _states)
                    if (st.InitialState == smState)
                        return (st[transition].SMSTATE);
                return (SMSTATE_CODE);
            }

            /// <summary>
            /// Follow "transition" going from "smState" and returns reached TokenSMSTATE.
            /// </summary>
            public int GetTokenSMSTATE(int smState, LexerTag transition)
            {
                foreach (State st in _states)
                    if (st.InitialState == smState)
                        return (st[transition].TokenSMSTATE);
                return (SMSTATE_CODE);
            }
        }

        #endregion
    }
}
