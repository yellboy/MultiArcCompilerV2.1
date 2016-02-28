/*
 * .grammarParser.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 */

using System.IO;

using PerCederberg.Grammatica.Runtime;

namespace MultiArc_Compiler {

    /**
     * <remarks>A token stream parser.</remarks>
     */
    public class .grammarParser : RecursiveDescentParser {

        /**
         * <summary>An enumeration with the generated production node
         * identity constants.</summary>
         */
        private enum SynteticPatterns {
            SUBPRODUCTION_1 = 3001,
            SUBPRODUCTION_2 = 3002
        }

        /**
         * <summary>Creates a new parser with a default analyzer.</summary>
         *
         * <param name='input'>the input stream to read from</param>
         *
         * <exception cref='ParserCreationException'>if the parser
         * couldn't be initialized correctly</exception>
         */
        public .grammarParser(TextReader input)
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
        public .grammarParser(TextReader input, .grammarAnalyzer analyzer)
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
            return new .grammarTokenizer(input);
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

            pattern = new ProductionPattern((int) .grammarConstants.PROGRAM,
                                            "Program");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) .grammarConstants.SEPARATOR, 0, 1);
            alt.AddProduction((int) .grammarConstants.ORIGIN, 0, 1);
            alt.AddProduction((int) .grammarConstants.LINES, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) .grammarConstants.SEPARATOR,
                                            "Separator");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.ENTER, 1, 1);
            alt.AddToken((int) .grammarConstants.ENTER, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) .grammarConstants.ORIGIN,
                                            "Origin");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.ORG, 1, 1);
            alt.AddToken((int) .grammarConstants.DEC_NUMBER, 1, 1);
            alt.AddProduction((int) .grammarConstants.SEPARATOR, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) .grammarConstants.LINES,
                                            "Lines");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) .grammarConstants.LINE, 1, 1);
            alt.AddProduction((int) .grammarConstants.LINE, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) .grammarConstants.LINE,
                                            "Line");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_1, 0, 1);
            alt.AddProduction((int) .grammarConstants.INSTRUCTION, 1, 1);
            alt.AddProduction((int) .grammarConstants.SEPARATOR, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) .grammarConstants.IMMED,
                                            "IMMED");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.HASH, 1, 1);
            alt.AddToken((int) .grammarConstants.IDENTIFIER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.HASH, 1, 1);
            alt.AddToken((int) .grammarConstants.SIGN, 0, 1);
            alt.AddToken((int) .grammarConstants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.HASH, 1, 1);
            alt.AddToken((int) .grammarConstants.SIGN, 0, 1);
            alt.AddToken((int) .grammarConstants.HEX_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) .grammarConstants.MEMDIR,
                                            "MEMDIR");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.IDENTIFIER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) .grammarConstants.REGDIR,
                                            "REGDIR");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.R0, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.R1, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.R2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.R3, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.R4, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.R5, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.R6, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.R7, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.R8, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.R9, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.R10, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.R11, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.R12, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.R13, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.R14, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.R15, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) .grammarConstants.REGIND,
                                            "REGIND");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) .grammarConstants.R0, 1, 1);
            alt.AddToken((int) .grammarConstants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) .grammarConstants.R1, 1, 1);
            alt.AddToken((int) .grammarConstants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) .grammarConstants.R2, 1, 1);
            alt.AddToken((int) .grammarConstants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) .grammarConstants.R3, 1, 1);
            alt.AddToken((int) .grammarConstants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) .grammarConstants.R4, 1, 1);
            alt.AddToken((int) .grammarConstants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) .grammarConstants.R5, 1, 1);
            alt.AddToken((int) .grammarConstants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) .grammarConstants.R6, 1, 1);
            alt.AddToken((int) .grammarConstants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) .grammarConstants.R7, 1, 1);
            alt.AddToken((int) .grammarConstants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) .grammarConstants.R8, 1, 1);
            alt.AddToken((int) .grammarConstants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) .grammarConstants.R9, 1, 1);
            alt.AddToken((int) .grammarConstants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) .grammarConstants.R10, 1, 1);
            alt.AddToken((int) .grammarConstants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) .grammarConstants.R11, 1, 1);
            alt.AddToken((int) .grammarConstants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) .grammarConstants.R12, 1, 1);
            alt.AddToken((int) .grammarConstants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) .grammarConstants.R13, 1, 1);
            alt.AddToken((int) .grammarConstants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) .grammarConstants.R14, 1, 1);
            alt.AddToken((int) .grammarConstants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) .grammarConstants.R15, 1, 1);
            alt.AddToken((int) .grammarConstants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) .grammarConstants.PCREL,
                                            "PCREL");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.IDENTIFIER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) .grammarConstants.LD1,
                                            "ld1");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.LD, 1, 1);
            alt.AddProduction((int) .grammarConstants.REGDIR, 1, 1);
            alt.AddToken((int) .grammarConstants.COMMA, 1, 1);
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_2, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) .grammarConstants.LD2,
                                            "ld2");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.LD, 1, 1);
            alt.AddProduction((int) .grammarConstants.REGDIR, 1, 1);
            alt.AddToken((int) .grammarConstants.COMMA, 1, 1);
            alt.AddProduction((int) .grammarConstants.REGIND, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) .grammarConstants.JMP1,
                                            "jmp1");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.JMP, 1, 1);
            alt.AddProduction((int) .grammarConstants.PCREL, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) .grammarConstants.HALT1,
                                            "halt1");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.HALT, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) .grammarConstants.INSTRUCTION,
                                            "Instruction");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) .grammarConstants.LD1, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) .grammarConstants.LD2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) .grammarConstants.JMP1, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) .grammarConstants.HALT1, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_1,
                                            "Subproduction1");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) .grammarConstants.IDENTIFIER, 1, 1);
            alt.AddToken((int) .grammarConstants.COLON, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_2,
                                            "Subproduction2");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) .grammarConstants.MEMDIR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) .grammarConstants.IMMED, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);
        }
    }
}
