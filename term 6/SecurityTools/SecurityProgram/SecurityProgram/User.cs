using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityProgram
{
    enum Roles
    {
        DiscordAdmin,
        Moderator,
        Redditor
    }

    internal class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Authorized { get; set; }
        public Roles Role { get; set; }
        public Roles DowngratedRole { get
            {
                switch (Role)
                {
                    case Roles.Redditor:
                    case Roles.Moderator:
                        return Roles.Redditor;
                    case Roles.DiscordAdmin:
                        return Roles.Moderator;
                    default:
                        return Role;
                }
            }
        }

        public User(string login, string password, bool authorized, Roles role)
        {
            this.Login = login;
            this.Password = password;
            this.Authorized = authorized;
            this.Role = role;
        }
    }
}
