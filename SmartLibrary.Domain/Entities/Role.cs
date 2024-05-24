﻿namespace SmartLibrary.Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
