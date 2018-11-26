using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VIA_Fitness.Models.Shared
{
    public class Workout
    {
        [Display(Name = "Which body part have you trained today?")]
        public string type { get; set; }
        [Display(Name = "How long was your workout?")]
        public int duration { get; set; }
        [Display(Name = "How many calories did you burned?")]
        public double caloriesBurned { get; set; }
        public DateTime date
        {
            get { return DateTime.Today; }
            set { date = value; }
        }
    }
}