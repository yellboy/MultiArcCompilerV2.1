/*
 * Arch4Parser.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 */

using System.IO;

using PerCederberg.Grammatica.Runtime;

namespace MultiArc_Compiler {

    /**
     * <remarks>A token stream parser.</remarks>
     */
    public class Arch4Parser : RecursiveDescentParser {

        /**
         * <summary>An enumeration with the generated production node
         * identity constants.</summary>
         */
        private enum SynteticPatterns {
            SUBPRODUCTION_1 = 3001,
            SUBPRODUCTION_2 = 3002,
            SUBPRODUCTION_3 = 3003,
            SUBPRODUCTION_4 = 3004
        }

        /**
         * <summary>Creates a new parser with a default analyzer.</summary>
         *
         * <param name='input'>the input stream to read from</param>
         *
         * <exception cref='ParserCreationException'>if the parser
         * couldn't be initialized correctly</exception>
         */
        public Arch4Parser(TextReader input)
            : base(input) {

            CreatePatterns();
        }

        /**
         * <summary>Creates a new parser.</summary>
         *
         * <param name='input'>the input stream to read from</param>
         *
         * <param name='analyzer'>the analyzer to parse with</param>
         *
         * <exception cref='ParserCreationException'>if the parser
         * couldn't be initialized correctly</exception>
         */
        public Arch4Parser(TextReader input, Arch4Analyzer analyzer)
            : base(input, analyzer) {

            CreatePatterns();
        }

        /**
         * <summary>Creates a new tokenizer for this parser. Can be overridden
         * by a subclass to provide a custom implementation.</summary>
         *
         * <param name='input'>the input stream to read from</param>
         *
         * <returns>the tokenizer created</returns>
         *
         * <exception cref='ParserCreationException'>if the tokenizer
         * couldn't be initialized correctly</exception>
         */
        protected override Tokenizer NewTokenizer(TextReader input) {
            return new Arch4Tokenizer(input);
        }

        /**
         * <summary>Initializes the parser by creating all the production
         * patterns.</summary>
         *
         * <exception cref='ParserCreationException'>if the parser
         * couldn't be initialized correctly</exception>
         */
        private void CreatePatterns() {
            ProductionPattern             pattern;
            ProductionPatternAlternative  alt;

            pattern = new ProductionPattern((int) Arch4Constants.PROGRAM,
                                            "Program");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Arch4Constants.SEPARATOR, 0, 1);
            alt.AddProduction((int) Arch4Constants.SYMBOLS, 1, 1);
            alt.AddProduction((int) Arch4Constants.ORIGIN, 1, 1);
            alt.AddProduction((int) Arch4Constants.LINES, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Arch4Constants.SEPARATOR,
                                            "Separator");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.ENTER, 1, 1);
            alt.AddToken((int) Arch4Constants.ENTER, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Arch4Constants.SYMBOLS,
                                            "Symbols");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Arch4Constants.SYMBOL, 1, 1);
            alt.AddProduction((int) Arch4Constants.SYMBOL, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Arch4Constants.SYMBOL,
                                            "Symbol");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.IDENTIFIER, 1, 1);
            alt.AddToken((int) Arch4Constants.EQUALS, 1, 1);
            alt.AddProduction((int) Arch4Constants.INTEGER, 1, 1);
            alt.AddProduction((int) Arch4Constants.SEPARATOR, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Arch4Constants.INTEGER,
                                            "Integer");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.SIGN, 0, 1);
            alt.AddToken((int) Arch4Constants.NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Arch4Constants.ORIGIN,
                                            "Origin");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.ORG, 1, 1);
            alt.AddToken((int) Arch4Constants.NUMBER, 1, 1);
            alt.AddProduction((int) Arch4Constants.SEPARATOR, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Arch4Constants.LINES,
                                            "Lines");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Arch4Constants.LINE, 1, 1);
            alt.AddProduction((int) Arch4Constants.LINE, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Arch4Constants.LINE,
                                            "Line");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_1, 0, 1);
            alt.AddProduction((int) Arch4Constants.INSTRUCTION, 1, 1);
            alt.AddProduction((int) Arch4Constants.SEPARATOR, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Arch4Constants.IMMED,
                                            "IMMED");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.HASH, 1, 1);
            alt.AddToken((int) Arch4Constants.IDENTIFIER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.HASH, 1, 1);
            alt.AddToken((int) Arch4Constants.NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Arch4Constants.MEMDIR,
                                            "MEMDIR");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.IDENTIFIER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Arch4Constants.REGDIR,
                                            "REGDIR");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_2, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Arch4Constants.REGIND,
                                            "REGIND");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.LEFT_PAREN, 1, 1);
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_3, 1, 1);
            alt.AddToken((int) Arch4Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Arch4Constants.LD1,
                                            "ld1");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.LD, 1, 1);
            alt.AddProduction((int) Arch4Constants.REGDIR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_4, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Arch4Constants.INSTRUCTION,
                                            "Instruction");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Arch4Constants.LD1, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_1,
                                            "Subproduction1");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.IDENTIFIER, 1, 1);
            alt.AddToken((int) Arch4Constants.COLON, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_2,
                                            "Subproduction2");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R0, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R1, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R3, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R4, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R5, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R6, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R7, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R8, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R9, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R10, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R11, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R12, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R13, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R14, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R15, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_3,
                                            "Subproduction3");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R0, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R1, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R3, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R4, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R5, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R6, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R7, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R8, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R9, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R10, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R11, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R12, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R13, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R14, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Arch4Constants.R15, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_4,
                                            "Subproduction4");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Arch4Constants.MEMDIR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Arch4Constants.IMMED, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);
        }
    }
}
