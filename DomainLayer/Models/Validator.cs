using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Models
{
    public static class Validator
    {
        /// <summary>
        /// Checks if the int? represents a vaild age or null
        /// </summary>
        /// <param name="a">The int? to be checked</param>
        /// <returns>False if it is negative, true in all other cases</returns>
        public static bool ValidateNullable(int? a)
        {
            if (a.HasValue)
            {
                if (a.Value > 0)
                {
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}
