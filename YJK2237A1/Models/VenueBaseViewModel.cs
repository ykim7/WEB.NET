using System;
using System.ComponentModel.DataAnnotations;

namespace YJK2237A1.Models
{
    public class VenueBaseViewModel : VenueAddViewModel
    {
        public int VenueId { get; set; }

        public string YearsOld
        {
            get
            {
                if (OpenDate.HasValue)
                {
                    var age = Math.Floor((DateTime.Now - OpenDate.Value).TotalDays / 365.0);
                    if (age < 1.0)
                    {
                        return "Recently opened";
                    }
                    else
                    {
                        return $"{age:n0} years old";
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
