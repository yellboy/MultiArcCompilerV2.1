/*
 * .grammarTokenizer.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 */

using System.IO;

using PerCederberg.Grammatica.Runtime;

namespace MultiArc_Compiler {

    /**
     * <remarks>A character stream tokenizer.</remarks>
     */
    public class .grammarTokenizer : Tokenizer {

        /**
         * <summary>Creates a new tokenizer for the specified input
         * stream.</summary>
         *
         * <param name='input'>the input stream to read</param>
         *
         * <exception cref='ParserCreationException'>if the tokenizer
         * couldn't be initialized correctly</exception>
         */
        public .grammarTokenizer(TextReader input)
            : base(input, true) {

            CreatePatterns();
        }

        /**
         * <summary>Initializes the tokenizer by creating all the token
         * patterns.</summary>
         *
         * <exception cref='ParserCreationException'>if the tokenizer
         * couldn't be initialized correctly</exception>
         */
        private void CreatePatterns() {
            TokenPattern  pattern;

            pattern = new TokenPattern((int) .grammarConstants.LD,
                                       "LD",
                                       TokenPattern.PatternType.STRING,
                                       "LD");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.ST,
                                       "ST",
                                       TokenPattern.PatternType.STRING,
                                       "ST");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.ADD,
                                       "ADD",
                                       TokenPattern.PatternType.STRING,
                                       "ADD");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.HALT,
                                       "HALT",
                                       TokenPattern.PatternType.STRING,
                                       "HALT");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.JMP,
                                       "JMP",
                                       TokenPattern.PatternType.STRING,
                                       "JMP");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.R0,
                                       "R0",
                                       TokenPattern.PatternType.STRING,
                                       "R0");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.R1,
                                       "R1",
                                       TokenPattern.PatternType.STRING,
                                       "R1");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.R2,
                                       "R2",
                                       TokenPattern.PatternType.STRING,
                                       "R2");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.R3,
                                       "R3",
                                       TokenPattern.PatternType.STRING,
                                       "R3");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.R4,
                                       "R4",
                                       TokenPattern.PatternType.STRING,
                                       "R4");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.R5,
                                       "R5",
                                       TokenPattern.PatternType.STRING,
                                       "R5");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.R6,
                                       "R6",
                                       TokenPattern.PatternType.STRING,
                                       "R6");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.R7,
                                       "R7",
                                       TokenPattern.PatternType.STRING,
                                       "R7");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.R8,
                                       "R8",
                                       TokenPattern.PatternType.STRING,
                                       "R8");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.R9,
                                       "R9",
                                       TokenPattern.PatternType.STRING,
                                       "R9");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.R10,
                                       "R10",
                                       TokenPattern.PatternType.STRING,
                                       "R10");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.R11,
                                       "R11",
                                       TokenPattern.PatternType.STRING,
                                       "R11");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.R12,
                                       "R12",
                                       TokenPattern.PatternType.STRING,
                                       "R12");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.R13,
                                       "R13",
                                       TokenPattern.PatternType.STRING,
                                       "R13");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.R14,
                                       "R14",
                                       TokenPattern.PatternType.STRING,
                                       "R14");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.R15,
                                       "R15",
                                       TokenPattern.PatternType.STRING,
                                       "R15");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.BL,
                                       "BL",
                                       TokenPattern.PatternType.STRING,
                                       "BL");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.BH,
                                       "BH",
                                       TokenPattern.PatternType.STRING,
                                       "BH");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.BX,
                                       "BX",
                                       TokenPattern.PatternType.STRING,
                                       "BX");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.PC,
                                       "PC",
                                       TokenPattern.PatternType.STRING,
                                       "PC");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.EQUALS,
                                       "EQUALS",
                                       TokenPattern.PatternType.STRING,
                                       "=");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.LEFT_PAREN,
                                       "LEFT_PAREN",
                                       TokenPattern.PatternType.STRING,
                                       "(");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.RIGHT_PAREN,
                                       "RIGHT_PAREN",
                                       TokenPattern.PatternType.STRING,
                                       ")");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.HASH,
                                       "HASH",
                                       TokenPattern.PatternType.STRING,
                                       "#");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.COLON,
                                       "COLON",
                                       TokenPattern.PatternType.STRING,
                                       ":");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.COMMA,
                                       "COMMA",
                                       TokenPattern.PatternType.STRING,
                                       ",");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.ORG,
                                       "ORG",
                                       TokenPattern.PatternType.STRING,
                                       "org");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.SIGN,
                                       "SIGN",
                                       TokenPattern.PatternType.REGEXP,
                                       "[+-]");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.DEC_NUMBER,
                                       "DEC_NUMBER",
                                       TokenPattern.PatternType.REGEXP,
                                       "[0-9]+");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.BIN_NUMBER,
                                       "BIN_NUMBER",
                                       TokenPattern.PatternType.REGEXP,
                                       "[01]+[bB]");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.OCT_NUMBER,
                                       "OCT_NUMBER",
                                       TokenPattern.PatternType.REGEXP,
                                       "[0-8]+[oO]");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.HEX_NUMBER,
                                       "HEX_NUMBER",
                                       TokenPattern.PatternType.REGEXP,
                                       "[0-9a-f]+[hH]");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.IDENTIFIER,
                                       "IDENTIFIER",
                                       TokenPattern.PatternType.REGEXP,
                                       "[a-z][a-z0-9_]*");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.ENTER,
                                       "ENTER",
                                       TokenPattern.PatternType.REGEXP,
                                       "[\\n\\r]+");
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.SINGLE_LINE_COMMENT,
                                       "SINGLE_LINE_COMMENT",
                                       TokenPattern.PatternType.REGEXP,
                                       ";.*");
            pattern.Ignore = true;
            AddPattern(pattern);

            pattern = new TokenPattern((int) .grammarConstants.WHITESPACE,
                                       "WHITESPACE",
                                       TokenPattern.PatternType.REGEXP,
                                       "[ \\t]+");
            pattern.Ignore = true;
            AddPattern(pattern);
        }
    }
}
