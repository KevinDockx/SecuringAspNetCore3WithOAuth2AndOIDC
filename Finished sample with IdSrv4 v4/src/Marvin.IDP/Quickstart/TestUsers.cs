// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServerHost.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
             new TestUser
             {
                 SubjectId = "d860efca-22d9-47fd-8249-791ba61b07c7",
                 Username = "Frank",
                 Password = "password",

                 Claims = new List<Claim>
                 {
                     new Claim("given_name", "Frank"),
                     new Claim("family_name", "Underwood"),
                     new Claim("address", "Main Road 1"),
                     new Claim("role", "FreeUser"),
                     new Claim("subscriptionlevel", "FreeUser"),
                     new Claim("country", "nl")
                 }
             },
             new TestUser
             {
                 SubjectId = "b7539694-97e7-4dfe-84da-b4256e1ff5c7",
                 Username = "Claire",
                 Password = "password",

                 Claims = new List<Claim>
                 {
                     new Claim("given_name", "Claire"),
                     new Claim("family_name", "Underwood"),
                     new Claim("address", "Big Street 2"),
                     new Claim("role", "PayingUser"),
                     new Claim("subscriptionlevel", "PayingUser"),
                     new Claim("country", "be")
                 }
             }
         };

    }
}