namespace APISecurityBasic.Models
{
    public class Villa
    {
        public Villa()
        {
           
        }

        public int Id { get; set; } 
        public string Name { get; set; } 
        public string Details { get; set; } 
        public double Rate { get; set; }
        public int Sqft { get; set; }
        public int Occupany { get; set; }
        public string ImageUrl { get; set; }    

        public string Amentity { get; set; }    


        
        public DateTime CreatedDate { get; set; } 
        
        public DateTime UpdatedDate { get; set;}
    }
}
