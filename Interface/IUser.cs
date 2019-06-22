using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trainingmiddleware.Model;

namespace trainingmiddleware.Interface
{
    public interface IUser
    {
        string Login(string email, string password);
        User Registration(string email, string password);
    }
}
