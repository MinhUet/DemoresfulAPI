using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nhom9.Docker_Kubernet.Demo.Entity.Entities
{
    public class BaseEntity
    {
        [BsonElement]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDate { get; set; }

        [BsonElement]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ModifiedDate { get; set; }

        [BsonElement]
        public string ModifiedBy { get; set; }

        [BsonElement]
        public string CreatedBy { get; set; }
    }
}
