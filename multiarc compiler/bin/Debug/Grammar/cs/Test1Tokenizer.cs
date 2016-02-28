/*
 * Test1Tokenizer.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 */

using System.IO;

using PerCederberg.Grammatica.Runtime;

namespace MultiArc_Compiler {

    /**
     * <remarks>A character stream tokenizer.</remarks>
     */
    public class Test1Tokenizer : Tokenizer {

        /**
         * <summary>Creates a new tokenizer for the specified input
         * stream.</summary>
         *
         * <param name='input'>the input stream to read</param>
         *
         * <exception cref='ParserCreationException'>if the tokenizer
         * couldn't be initialized correctly</exception>
         */
        public Test1Tokenizer(TextReader input)
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

            pattern = new TokenPattern((int) Test1Constants.TOKEN1,
                                       "token1",
                                       TokenPattern.PatternType.STRING,
                                       "+(");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.TOKEN2,
                                       "token2",
                                       TokenPattern.PatternType.STRING,
                                       ")-");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.TOKEN3,
                                       "token3",
                                       TokenPattern.PatternType.STRING,
                                       "[");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.TOKEN4,
                                       "token4",
                                       TokenPattern.PatternType.STRING,
                                       "]");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.LD,
                                       "LD",
                                       TokenPattern.PatternType.STRING,
                                       "LD");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.BZ,
                                       "BZ",
                                       TokenPattern.PatternType.STRING,
                                       "BZ");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.ST,
                                       "ST",
                                       TokenPattern.PatternType.STRING,
                                       "ST");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.ADD,
                                       "ADD",
                                       TokenPattern.PatternType.STRING,
                                       "ADD");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.AND,
                                       "AND",
                                       TokenPattern.PatternType.STRING,
                                       "AND");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.JADR,
                                       "JADR",
                                       TokenPattern.PatternType.STRING,
                                       "JADR");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.ASR,
                                       "ASR",
                                       TokenPattern.PatternType.STRING,
                                       "ASR");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.JMP,
                                       "JMP",
                                       TokenPattern.PatternType.STRING,
                                       "JMP");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.JSR,
                                       "JSR",
                                       TokenPattern.PatternType.STRING,
                                       "JSR");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.PUSH,
                                       "PUSH",
                                       TokenPattern.PatternType.STRING,
                                       "PUSH");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.POP,
                                       "POP",
                                       TokenPattern.PatternType.STRING,
                                       "POP");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.RTS,
                                       "RTS",
                                       TokenPattern.PatternType.STRING,
                                       "RTS");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.PC,
                                       "PC",
                                       TokenPattern.PatternType.STRING,
                                       "PC");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.A,
                                       "A",
                                       TokenPattern.PatternType.STRING,
                                       "A");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R0,
                                       "R0",
                                       TokenPattern.PatternType.STRING,
                                       "R0");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R1,
                                       "R1",
                                       TokenPattern.PatternType.STRING,
                                       "R1");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R2,
                                       "R2",
                                       TokenPattern.PatternType.STRING,
                                       "R2");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R3,
                                       "R3",
                                       TokenPattern.PatternType.STRING,
                                       "R3");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R4,
                                       "R4",
                                       TokenPattern.PatternType.STRING,
                                       "R4");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R5,
                                       "R5",
                                       TokenPattern.PatternType.STRING,
                                       "R5");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R6,
                                       "R6",
                                       TokenPattern.PatternType.STRING,
                                       "R6");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R7,
                                       "R7",
                                       TokenPattern.PatternType.STRING,
                                       "R7");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R8,
                                       "R8",
                                       TokenPattern.PatternType.STRING,
                                       "R8");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R9,
                                       "R9",
                                       TokenPattern.PatternType.STRING,
                                       "R9");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R10,
                                       "R10",
                                       TokenPattern.PatternType.STRING,
                                       "R10");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R11,
                                       "R11",
                                       TokenPattern.PatternType.STRING,
                                       "R11");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R12,
                                       "R12",
                                       TokenPattern.PatternType.STRING,
                                       "R12");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R13,
                                       "R13",
                                       TokenPattern.PatternType.STRING,
                                       "R13");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R14,
                                       "R14",
                                       TokenPattern.PatternType.STRING,
                                       "R14");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R15,
                                       "R15",
                                       TokenPattern.PatternType.STRING,
                                       "R15");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R16,
                                       "R16",
                                       TokenPattern.PatternType.STRING,
                                       "R16");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R17,
                                       "R17",
                                       TokenPattern.PatternType.STRING,
                                       "R17");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R18,
                                       "R18",
                                       TokenPattern.PatternType.STRING,
                                       "R18");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R19,
                                       "R19",
                                       TokenPattern.PatternType.STRING,
                                       "R19");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R20,
                                       "R20",
                                       TokenPattern.PatternType.STRING,
                                       "R20");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R21,
                                       "R21",
                                       TokenPattern.PatternType.STRING,
                                       "R21");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R22,
                                       "R22",
                                       TokenPattern.PatternType.STRING,
                                       "R22");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R23,
                                       "R23",
                                       TokenPattern.PatternType.STRING,
                                       "R23");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R24,
                                       "R24",
                                       TokenPattern.PatternType.STRING,
                                       "R24");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R25,
                                       "R25",
                                       TokenPattern.PatternType.STRING,
                                       "R25");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R26,
                                       "R26",
                                       TokenPattern.PatternType.STRING,
                                       "R26");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R27,
                                       "R27",
                                       TokenPattern.PatternType.STRING,
                                       "R27");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R28,
                                       "R28",
                                       TokenPattern.PatternType.STRING,
                                       "R28");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R29,
                                       "R29",
                                       TokenPattern.PatternType.STRING,
                                       "R29");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R30,
                                       "R30",
                                       TokenPattern.PatternType.STRING,
                                       "R30");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.R31,
                                       "R31",
                                       TokenPattern.PatternType.STRING,
                                       "R31");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.PSW,
                                       "PSW",
                                       TokenPattern.PatternType.STRING,
                                       "PSW");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.SP,
                                       "SP",
                                       TokenPattern.PatternType.STRING,
                                       "SP");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.EQUALS,
                                       "EQUALS",
                                       TokenPattern.PatternType.STRING,
                                       "=");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.LEFT_PAREN,
                                       "LEFT_PAREN",
                                       TokenPattern.PatternType.STRING,
                                       "(");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.RIGHT_PAREN,
                                       "RIGHT_PAREN",
                                       TokenPattern.PatternType.STRING,
                                       ")");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.HASH,
                                       "HASH",
                                       TokenPattern.PatternType.STRING,
                                       "#");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.COLON,
                                       "COLON",
                                       TokenPattern.PatternType.STRING,
                                       ":");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.COMMA,
                                       "COMMA",
                                       TokenPattern.PatternType.STRING,
                                       ",");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.ORG,
                                       "ORG",
                                       TokenPattern.PatternType.STRING,
                                       "org");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.SIGN,
                                       "SIGN",
                                       TokenPattern.PatternType.REGEXP,
                                       "[+-]");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.DEC_NUMBER,
                                       "DEC_NUMBER",
                                       TokenPattern.PatternType.REGEXP,
                                       "[0-9]+");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.BIN_NUMBER,
                                       "BIN_NUMBER",
                                       TokenPattern.PatternType.REGEXP,
                                       "[01]+[bB]");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.OCT_NUMBER,
                                       "OCT_NUMBER",
                                       TokenPattern.PatternType.REGEXP,
                                       "[0-8]+[oO]");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.HEX_NUMBER,
                                       "HEX_NUMBER",
                                       TokenPattern.PatternType.REGEXP,
                                       "[0-9a-f]+[hH]");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.IDENTIFIER,
                                       "IDENTIFIER",
                                       TokenPattern.PatternType.REGEXP,
                                       "[a-z][a-z0-9_]*");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.ENTER,
                                       "ENTER",
                                       TokenPattern.PatternType.REGEXP,
                                       "[\\n\\r]+");
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.SINGLE_LINE_COMMENT,
                                       "SINGLE_LINE_COMMENT",
                                       TokenPattern.PatternType.REGEXP,
                                       ";.*");
            pattern.Ignore = true;
            AddPattern(pattern);

            pattern = new TokenPattern((int) Test1Constants.WHITESPACE,
                                       "WHITESPACE",
                                       TokenPattern.PatternType.REGEXP,
                                       "[ \\t]+");
            pattern.Ignore = true;
            AddPattern(pattern);
        }
    }
}
