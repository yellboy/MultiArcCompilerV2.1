/*
 * File: Variables.cs
 * Author: Bojan Jelaca
 * Date: August 2014
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiArc_Compiler
{
    /// <summary>
    /// User variables.
    /// </summary>
    public class Variables
    {

        /// <summary>
        /// Dictionary containing pairs of names and values for variables.
        /// </summary>
        private Dictionary<string, object> variables = new Dictionary<string,object>();

        /// <summary>
        /// Creates one instance of Variables class.
        /// </summary>
        public Variables()
        {
            variables.Clear();
            variables.Add("working", false);
        }

        /// <summary>
        /// Clears variables.
        /// </summary>
        public void Clear()
        {
            variables.Clear();
            variables.Add("working", false);
        }

        /// <summary>
        /// Gets one variable.
        /// </summary>
        /// <param name="name">
        /// Name of the variable.
        /// </param>
        /// <returns>
        /// Value of the variable.
        /// </returns>
        public object GetVariable(string name)
        {
            return variables[name];
        }

        /// <summary>
        /// Sets value of the variable if it exists or creates new variable if it does not.
        /// </summary>
        /// <param name="name">
        /// Name of the variable.
        /// </param>
        /// <param name="value">
        /// Value of the variable.
        /// </param>
        public void SetVariable(string name, object value)
        {
            if (variables.ContainsKey(name))
            {
                variables[name] = value;
            }
            else
            {
                variables.Add(name, value);
            }
        }
    }
}
