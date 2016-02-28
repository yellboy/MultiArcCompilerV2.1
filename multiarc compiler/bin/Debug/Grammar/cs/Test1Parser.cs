/*
 * Test1Parser.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 */

using System.IO;

using PerCederberg.Grammatica.Runtime;

namespace MultiArc_Compiler {

    /**
     * <remarks>A token stream parser.</remarks>
     */
    public class Test1Parser : RecursiveDescentParser {

        /**
         * <summary>An enumeration with the generated production node
         * identity constants.</summary>
         */
        private enum SynteticPatterns {
            SUBPRODUCTION_1 = 3001,
            SUBPRODUCTION_2 = 3002,
            SUBPRODUCTION_3 = 3003,
            SUBPRODUCTION_4 = 3004,
            SUBPRODUCTION_5 = 3005,
            SUBPRODUCTION_6 = 3006,
            SUBPRODUCTION_7 = 3007,
            SUBPRODUCTION_8 = 3008,
            SUBPRODUCTION_9 = 3009,
            SUBPRODUCTION_10 = 3010,
            SUBPRODUCTION_11 = 3011,
            SUBPRODUCTION_12 = 3012,
            SUBPRODUCTION_13 = 3013
        }

        /**
         * <summary>Creates a new parser with a default analyzer.</summary>
         *
         * <param name='input'>the input stream to read from</param>
         *
         * <exception cref='ParserCreationException'>if the parser
         * couldn't be initialized correctly</exception>
         */
        public Test1Parser(TextReader input)
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
        public Test1Parser(TextReader input, Test1Analyzer analyzer)
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
            return new Test1Tokenizer(input);
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

            pattern = new ProductionPattern((int) Test1Constants.PROGRAM,
                                            "Program");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.SEPARATOR, 0, 1);
            alt.AddProduction((int) Test1Constants.ORIGIN, 0, 1);
            alt.AddProduction((int) Test1Constants.LINES, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.SEPARATOR,
                                            "Separator");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.ENTER, 1, 1);
            alt.AddToken((int) Test1Constants.ENTER, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.ORIGIN,
                                            "Origin");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.ORG, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            alt.AddProduction((int) Test1Constants.SEPARATOR, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.LINES,
                                            "Lines");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.LINE, 1, 1);
            alt.AddProduction((int) Test1Constants.LINE, 0, -1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.LINE,
                                            "Line");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_1, 0, 1);
            alt.AddProduction((int) Test1Constants.INSTRUCTION, 1, 1);
            alt.AddProduction((int) Test1Constants.SEPARATOR, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.JMPADDR,
                                            "JMPADDR");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.IDENTIFIER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.REGDIR,
                                            "REGDIR");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R0, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R1, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R3, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R4, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R5, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R6, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R7, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R8, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R9, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R10, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R11, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R12, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R13, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R14, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R15, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R16, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R17, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R18, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R19, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R20, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R21, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R22, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R23, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R24, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R25, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R26, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R27, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R28, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R29, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R30, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.R31, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.REGIND,
                                            "REGIND");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R0, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R1, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R2, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R3, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R4, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R5, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R6, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R7, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R8, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R9, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R10, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R11, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R12, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R13, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R14, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R15, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R16, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R17, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R18, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R19, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R20, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R21, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R22, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R23, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R24, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R25, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R26, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R27, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R28, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R29, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R30, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R31, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.POSTDECR,
                                            "POSTDECR");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R0, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R1, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R2, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R3, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R4, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R5, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R6, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R7, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R8, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R9, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R10, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R11, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R12, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R13, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R14, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R15, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R16, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R17, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R18, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R19, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R20, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R21, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R22, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R23, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R24, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R25, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R26, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R27, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R28, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R29, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R30, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.R31, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN2, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.PREINCR,
                                            "PREINCR");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R0, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R1, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R2, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R3, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R4, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R5, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R6, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R7, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R8, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R9, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R10, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R11, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R12, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R13, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R14, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R15, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R16, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R17, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R18, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R19, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R20, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R21, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R22, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R23, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R24, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R25, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R26, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R27, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R28, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R29, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R30, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN1, 1, 1);
            alt.AddToken((int) Test1Constants.R31, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.MEMDIR,
                                            "MEMDIR");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.IDENTIFIER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.MEMIND,
                                            "MEMIND");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.IDENTIFIER, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LEFT_PAREN, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            alt.AddToken((int) Test1Constants.RIGHT_PAREN, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.REGINDPOM,
                                            "REGINDPOM");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R0, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R1, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R2, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R3, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R4, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R5, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R6, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R7, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R8, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R9, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R10, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R11, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R12, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R13, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R14, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R15, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R16, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R17, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R18, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R19, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R20, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R21, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R22, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R23, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R24, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R25, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R26, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R27, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R28, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R29, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R30, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.TOKEN3, 1, 1);
            alt.AddToken((int) Test1Constants.R31, 1, 1);
            alt.AddToken((int) Test1Constants.TOKEN4, 1, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.IMMED,
                                            "IMMED");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.HASH, 1, 1);
            alt.AddToken((int) Test1Constants.IDENTIFIER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.HASH, 1, 1);
            alt.AddToken((int) Test1Constants.SIGN, 0, 1);
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.DISP,
                                            "DISP");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.DEC_NUMBER, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.IDENTIFIER, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.LD1,
                                            "ld1");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LD, 1, 1);
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_2, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.LD2,
                                            "ld2");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LD, 1, 1);
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_3, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.LD3,
                                            "ld3");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LD, 1, 1);
            alt.AddProduction((int) Test1Constants.REGINDPOM, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.LD4,
                                            "ld4");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.LD, 1, 1);
            alt.AddProduction((int) Test1Constants.IMMED, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.ST1,
                                            "st1");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.ST, 1, 1);
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_4, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.ST2,
                                            "st2");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.ST, 1, 1);
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_5, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.ST3,
                                            "st3");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.ST, 1, 1);
            alt.AddProduction((int) Test1Constants.REGINDPOM, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.BZ1,
                                            "bz1");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.BZ, 1, 1);
            alt.AddProduction((int) Test1Constants.DISP, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.ADD1,
                                            "add1");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.ADD, 1, 1);
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_6, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.ADD2,
                                            "add2");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.ADD, 1, 1);
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_7, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.ADD3,
                                            "add3");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.ADD, 1, 1);
            alt.AddProduction((int) Test1Constants.REGINDPOM, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.ADD4,
                                            "add4");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.ADD, 1, 1);
            alt.AddProduction((int) Test1Constants.IMMED, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.AND1,
                                            "and1");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.AND, 1, 1);
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_8, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.AND2,
                                            "and2");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.AND, 1, 1);
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_9, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.AND3,
                                            "and3");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.AND, 1, 1);
            alt.AddProduction((int) Test1Constants.REGINDPOM, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.AND4,
                                            "and4");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.AND, 1, 1);
            alt.AddProduction((int) Test1Constants.IMMED, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.ASR1,
                                            "asr1");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.ASR, 1, 1);
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_10, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.ASR2,
                                            "asr2");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.ASR, 1, 1);
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_11, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.ASR3,
                                            "asr3");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.ASR, 1, 1);
            alt.AddProduction((int) Test1Constants.REGINDPOM, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.ASR4,
                                            "asr4");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.ASR, 1, 1);
            alt.AddProduction((int) Test1Constants.IMMED, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.JADR1,
                                            "jadr1");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.JADR, 1, 1);
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_12, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.JADR2,
                                            "jadr2");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.JADR, 1, 1);
            alt.AddProduction((int) SynteticPatterns.SUBPRODUCTION_13, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.JADR3,
                                            "jadr3");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.JADR, 1, 1);
            alt.AddProduction((int) Test1Constants.REGINDPOM, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.JMP1,
                                            "jmp1");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.JMP, 1, 1);
            alt.AddProduction((int) Test1Constants.JMPADDR, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.JSR1,
                                            "jsr1");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.JSR, 1, 1);
            alt.AddProduction((int) Test1Constants.MEMDIR, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.PUSH1,
                                            "push1");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.PUSH, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.POP1,
                                            "pop1");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.POP, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.RTS1,
                                            "rts1");
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.RTS, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) Test1Constants.INSTRUCTION,
                                            "Instruction");
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.LD1, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.LD2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.LD3, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.LD4, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.ST1, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.ST2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.ST3, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.BZ1, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.ADD1, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.ADD2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.ADD3, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.ADD4, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.AND1, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.AND2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.AND3, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.AND4, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.ASR1, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.ASR2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.ASR3, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.ASR4, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.JADR1, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.JADR2, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.JADR3, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.JMP1, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.JSR1, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.PUSH1, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.POP1, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.RTS1, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_1,
                                            "Subproduction1");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddToken((int) Test1Constants.IDENTIFIER, 1, 1);
            alt.AddToken((int) Test1Constants.COLON, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_2,
                                            "Subproduction2");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.REGDIR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.REGIND, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.POSTDECR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.PREINCR, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_3,
                                            "Subproduction3");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.MEMDIR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.MEMIND, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_4,
                                            "Subproduction4");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.REGDIR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.REGIND, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.POSTDECR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.PREINCR, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_5,
                                            "Subproduction5");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.MEMDIR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.MEMIND, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_6,
                                            "Subproduction6");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.REGDIR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.REGIND, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.POSTDECR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.PREINCR, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_7,
                                            "Subproduction7");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.MEMDIR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.MEMIND, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_8,
                                            "Subproduction8");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.REGDIR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.REGIND, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.POSTDECR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.PREINCR, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_9,
                                            "Subproduction9");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.MEMDIR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.MEMIND, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_10,
                                            "Subproduction10");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.REGDIR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.REGIND, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.POSTDECR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.PREINCR, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_11,
                                            "Subproduction11");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.MEMDIR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.MEMIND, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_12,
                                            "Subproduction12");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.REGDIR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.REGIND, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.POSTDECR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.PREINCR, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);

            pattern = new ProductionPattern((int) SynteticPatterns.SUBPRODUCTION_13,
                                            "Subproduction13");
            pattern.Synthetic = true;
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.MEMDIR, 1, 1);
            pattern.AddAlternative(alt);
            alt = new ProductionPatternAlternative();
            alt.AddProduction((int) Test1Constants.MEMIND, 1, 1);
            pattern.AddAlternative(alt);
            AddPattern(pattern);
        }
    }
}
