using System;

namespace Api.Domain.Models
{
    public class BaseModel
    {
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        private DateTime _createAt;
        public DateTime CreateAt
        {
            get { return _createAt; }
            set { 
                    _createAt = value == null ? DateTime.UtcNow : value; 
                }
        }
        
        private DateTime _upadateAt;
        public DateTime UpdateAt
        {
            get { return _upadateAt; }
            set { _upadateAt = value; }
        }       
        
    }
}
