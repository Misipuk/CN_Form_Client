using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN_Form_Client
{
    class Review
    {
        public int id;
        public string owner;
        public int cafe_id;
        public string time;
        public int stars;
        public string description;
        public Review(string owner, int cafe_id, int stars, string descr)
        {
            this.owner = owner;
            this.cafe_id = cafe_id;
            this.stars = stars;
            this.description = descr;
        }
    }
}
