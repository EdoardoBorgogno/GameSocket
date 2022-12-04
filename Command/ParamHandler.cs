using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAlpha.Command
{
    /// <summary>
    /// Provides static method for parameter checking and setting.
    /// </summary>
    internal static class ParamHandler
    {
        /// <summary>
        /// Get value of param in array.
        /// </summary>
        /// <param name="parameters">Array with parameters.</param>
        /// <param name="paramName">Name of param to search.</param>
        /// <param name="outValue">Param value or Error message.</param>
        /// <param name="startWithDash">Value can start with dash.</param>
        /// <returns>True if param have correct value, in this case
        /// outValue contains param value, otherwise false, in this case 
        /// outValue contains message error.</returns>
        public static bool getParamValue(string[] parameters, string paramName, out string outValue, bool? startWithDash = false)
        {
            //Variables
            bool valueIsPresent; //==> if true value is present in parameters array, returned variable
            bool paramIsPresent; //==> if true param is present in parameters array

            //Check if param is present
            paramIsPresent = parameters.Contains(paramName);

            //if param is present
            if (paramIsPresent)
            {
                //Get index of param in parameters Array
                int paramIndex = Array.IndexOf(parameters, paramName);

                // If paramIndex isn't last item in array
                int arrayLength = parameters.Length - 1;
                if (arrayLength != paramIndex)
                {
                    //Get next Value in parameters Array
                    string nextValue = parameters[paramIndex + 1];

                    //
                    // Check Next Value
                    //

                    // Next Value is another param?
                    bool nextValueIsParam = nextValue.StartsWith("-"); //==> param always starts with dash

                    // If next value isn't a param or startWithDash option is true
                    if (nextValueIsParam == false || startWithDash == true)
                    {
                        valueIsPresent = true;
                        outValue = parameters[paramIndex + 1]; //==> set value
                    }
                    else
                    {
                        // If next value start with dash
                        valueIsPresent = false;
                        outValue = "Param is present but there is no value for it.";
                    }
                }
                else
                {
                    // If paramName is the last item in parameters Array.
                    valueIsPresent = false;
                    outValue = "Param is present but there is no value for it.";
                }
            }
            else
            {
                //If param isn't in array
                valueIsPresent = false;
                outValue = "Param is not in array.";
            }

            //Return
            return valueIsPresent;
        }
    }
}
