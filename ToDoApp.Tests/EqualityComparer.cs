using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using ToDoApp.DAL.Entities;

namespace ToDoApp.Tests
{
    internal class ToDoEqualityComparer : IEqualityComparer<ToDo> 
    {
        public bool Equals([AllowNull] ToDo x, [AllowNull] ToDo y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id== y.Id
                && x.Text == y.Text
                && x.AppUserId == y.AppUserId;
        }

        public int GetHashCode([DisallowNull] ToDo obj)
        {
            return obj.GetHashCode();
        }
    }
}
