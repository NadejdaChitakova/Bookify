﻿namespace Bookify.Domain.Users
{
    public sealed class Permission(
        int id, 
        string name)
    {
        public static readonly Permission UsersRead = new(1, "user:read");

        public int Id { get; init; }

        public string Name { get; init; }
    }
}
