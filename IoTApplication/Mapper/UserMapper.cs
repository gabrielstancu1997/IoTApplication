﻿using IoTApplication.Enums;
using IoTApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTApplication.Mapper
{
    public static class UserMapper
    {
        public static User ToUser(AuthenticationRequest request, UserTypeEnum userType)
        {
            return new User
            {
                Username = request.Username,
                Password = request.Password,
                Type = userType.ToString()
            };
        }

        public static User ToUserExtension(this AuthenticationRequest request, UserTypeEnum userType)
        {
            return new User
            {
                Username = request.Username,
                Password = request.Password,
                Type = userType.ToString()
            };
        }
    }
}
