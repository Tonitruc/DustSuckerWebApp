using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class AddReviewDto
    {
        public string UserEmail {  get; set; }

        public int Rating { get; set; }

        public string Text { get; set; }
    }
}
