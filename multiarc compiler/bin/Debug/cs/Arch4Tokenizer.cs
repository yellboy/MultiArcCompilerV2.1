/*
 * Arch4Tokenizer.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 */

using System.IO;

using PerCederberg.Grammatica.Runtime;

namespace MultiArc_Compiler {

    /**
     * <remarks>A character stream tokenizer.</remarks>
     */
    internal class Arch4Tokenizer : Tokenizer {

        /**
         * <summary>Creates a new tokenizer for the specified input
         * stream.</summary>
         *
         * <param name='input'>the input stream to read</param>
         *
         * <exception cref='ParserCreationException'>if the tokenizer
         * couldn't be initialized correctly</exception>
         */
        public Arch4Tokenizer(TextReader input)
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

            pattern = new TokenPattern((int) Arch4Constants.EQUALS,
                                       "EQUALS",
                                       TokenPattern.PatternType.STRING,
                                       "=");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.LEFT_PAREN,
                                       "LEFT_PAREN",
                                       TokenPattern.PatternType.STRING,
                                       "(");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.RIGHT_PAREN,
                                       "RIGHT_PAREN",
                                       TokenPattern.PatternType.STRING,
                                       ")");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.HASH,
                                       "HASH",
                                       TokenPattern.PatternType.STRING,
                                       "#");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.COLON,
                                       "COLON",
                                       TokenPattern.PatternType.STRING,
                                       ":");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.COMMA,
                                       "COMMA",
                                       TokenPattern.PatternType.STRING,
                                       ",");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.SIGN,
                                       "SIGN",
                                       TokenPattern.PatternType.REGEXP,
                                       "[+-]");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.NUMBER,
                                       "NUMBER",
                                       TokenPattern.PatternType.REGEXP,
                                       "[0-9]+");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.IDENTIFIER,
                                       "IDENTIFIER",
                                       TokenPattern.PatternType.REGEXP,
                                       "[a-z][a-z0-9_]*");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.ENTER,
                                       "ENTER",
                                       TokenPattern.PatternType.REGEXP,
                                       "[\\n\\r]+");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.SINGLE_LINE_COMMENT,
                                       "SINGLE_LINE_COMMENT",
                                       TokenPattern.PatternType.REGEXP,
                                       ";.*");
            pattern.Ignore = true;
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.WHITESPACE,
                                       "WHITESPACE",
                                       TokenPattern.PatternType.REGEXP,
                                       "[ \\t]+");
            pattern.Ignore = true;
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.ORG,
                                       "ORG",
                                       TokenPattern.PatternType.STRING,
                                       "org");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.LD,
                                       "LD",
                                       TokenPattern.PatternType.STRING,
                                       "LD");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.ST,
                                       "ST",
                                       TokenPattern.PatternType.STRING,
                                       "ST");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.ADD,
                                       "ADD",
                                       TokenPattern.PatternType.STRING,
                                       "ADD");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.HALT,
                                       "HALT",
                                       TokenPattern.PatternType.STRING,
                                       "HALT");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.R0,
                                       "R0",
                                       TokenPattern.PatternType.STRING,
                                       "R0");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.R1,
                                       "R1",
                                       TokenPattern.PatternType.STRING,
                                       "R1");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.R2,
                                       "R2",
                                       TokenPattern.PatternType.STRING,
                                       "R2");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.R3,
                                       "R3",
                                       TokenPattern.PatternType.STRING,
                                       "R3");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.R4,
                                       "R4",
                                       TokenPattern.PatternType.STRING,
                                       "R4");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.R5,
                                       "R5",
                                       TokenPattern.PatternType.STRING,
                                       "R5");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.R6,
                                       "R6",
                                       TokenPattern.PatternType.STRING,
                                       "R6");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.R7,
                                       "R7",
                                       TokenPattern.PatternType.STRING,
                                       "R7");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.R8,
                                       "R8",
                                       TokenPattern.PatternType.STRING,
                                       "R8");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.R9,
                                       "R9",
                                       TokenPattern.PatternType.STRING,
                                       "R9");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.R10,
                                       "R10",
                                       TokenPattern.PatternType.STRING,
                                       "R10");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.R11,
                                       "R11",
                                       TokenPattern.PatternType.STRING,
                                       "R11");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.R12,
                                       "R12",
                                       TokenPattern.PatternType.STRING,
                                       "R12");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.R13,
                                       "R13",
                                       TokenPattern.PatternType.STRING,
                                       "R13");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.R14,
                                       "R14",
                                       TokenPattern.PatternType.STRING,
                                       "R14");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.R15,
                                       "R15",
                                       TokenPattern.PatternType.STRING,
                                       "R15");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.BL,
                                       "BL",
                                       TokenPattern.PatternType.STRING,
                                       "BL");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.BH,
                                       "BH",
                                       TokenPattern.PatternType.STRING,
                                       "BH");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.BX,
                                       "BX",
                                       TokenPattern.PatternType.STRING,
                                       "BX");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Arch4Constants.PC,
                                       "PC",
                                       TokenPattern.PatternType.STRING,
                                       "PC");
            AddPattern(pattern);
        }
    }
}
