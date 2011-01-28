using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace GenSyntax
{
    class SyntaxInfo : List<Stanza>
    {
        private static SyntaxInfo instance = new SyntaxInfo();

        public static SyntaxInfo Instance
        {
            get { return instance; }
        }

        public void Load(StreamReader input)
        {
            this.Clear();

            while (!input.EndOfStream)
            {
                Stanza stanza = Stanza.Read(input);
                this.Add(stanza);
            }
        }

        private List<string> defaults;
        public List<string> Defaults
        {
            get
            {
                if (defaults == null)
                {
                    defaults = new List<string>();
                    foreach (Stanza stanza in this)
                        foreach (string option in stanza.Defaults)
                            defaults.Add(option);
                }

                return defaults;
            }
        }
    }
}
