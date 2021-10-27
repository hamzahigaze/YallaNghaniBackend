using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YallaNghani.Models.Core
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        [BsonId()]
        public string Id { get; set; }

        public void FillMissingProperties(Entity entity)
        {
            if (entity is null)
                return;

            if (GetType() != entity.GetType())
                return;

            var entityType = GetType();
            var properties = entityType.GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(this);
                var propertyType = property.PropertyType;
                var hasDefaultConstructor = propertyType.GetConstructor(Type.EmptyTypes) != null || propertyType.IsValueType;
                if (propertyValue == default || (hasDefaultConstructor && propertyValue.Equals(Activator.CreateInstance(propertyType))))
                {
                    property.SetValue(this, property.GetValue(entity));
                }
            }
        }
    }
}
